// ReSharper disable InvokeAsExtensionMethod
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using Hazzik.Data;
using Hazzik.Data.NH;
using Hazzik.Helper;

namespace Hazzik.Net {
	public class AuthPacketProcessor : IPacketProcessor {
		private static readonly BigInteger bi_g = 7;
		private static readonly BigInteger bi_k = 3;

		private static readonly BigInteger bi_N =
			new BigInteger("894B645E89E1535BBDAD5B8B290650530801B18EBFBF5E8FAB3C82872A3E9BB7", 16);

		private static readonly SHA1 sha1 = SHA1.Create();
		private readonly AuthClient _client;
		private readonly IAccountDao _dao = new NHAccountRepository();
		private readonly IList<WorldServerInfo> _realmList = new List<WorldServerInfo>();
		private readonly BigInteger bi_b = BigInteger.genPseudoPrime(160, 5, Utility.seed2);
		public Account _account;
		private BigInteger bi_B;
		private BigInteger bi_s = BigInteger.genPseudoPrime(256, 5, Utility.seed2);
		private BigInteger bi_v;

		public AuthPacketProcessor(AuthClient client) {
			_client = client;
			_realmList = new List<WorldServerInfo> {
				new WorldServerInfo {
					Type = 0,
					Locked = false,
					Status = 0,
					Name = "TestRealm",
					Address = "127.0.0.1:3725",
					CharactersCount = 1,
					Language = 8,
					Population = 1,
					Unk = 0
				},
			};
		}

		public ClientInfo ClientInfo { get; set; }

		public bool AcceptPatch { get; set; }

		#region IPacketProcessor Members

		public void Process(IPacket packet) {
			switch((RMSG)packet.Code) {
			case RMSG.AUTH_LOGON_CHALLENGE:
			case RMSG.AUTH_LOGON_RECODE_CHALLENGE:
				HandleLogonChallenge(packet);
				break;
			case RMSG.AUTH_LOGON_PROOF:
			case RMSG.AUTH_LOGON_RECODE_PROOF:
				HandleLogonProof(packet);
				break;
			case RMSG.REALM_LIST:
				HandleRealmList();
				break;
			case RMSG.XFER_ACCEPT:
				HandleXferAccept();
				break;
			case RMSG.XFER_RESUME:
				HandleXferResume(packet);
				break;
			case RMSG.XFER_CANCEL:
				HandleXferCancel();
				break;
			}
		}

		#endregion

		private void HandleLogonChallenge(IPacket packet) {
			BinaryReader gr = packet.CreateReader();
			string tag = gr.ReadCString();
			var verMajor = (int)gr.ReadByte();
			var verMinor = (int)gr.ReadByte();
			var verBuild = (int)gr.ReadByte();
			var verRevis = (int)gr.ReadUInt16();
			string platform = gr.ReadCString();
			string os = gr.ReadCString();
			string locale = Encoding.UTF8.GetString(gr.ReadBytes(4).Reverse());
			int timezone = gr.ReadInt32();
			var ip = new IPAddress(gr.ReadBytes(4));
			string accountName = gr.ReadString();

			ClientInfo = new ClientInfo {
				VersionInfo = new VersionInfo {
					ClientTag = tag,
					Version = new Version(verMajor, verMinor, verBuild, verRevis),
					Platform = platform,
					OS = os,
					Locale = locale,
				},
				TimeZone = timezone,
				IP = ip,
				AccountName = accountName,
			};

			_account = _dao.FindByName(accountName);
			if(_account == null) {
				_account = new Account { Name = accountName };
				_account.SetPassword(accountName);
				_dao.Save(_account);
				_dao.SubmitChanges();
			}

			bi_s = new BigInteger(_account.PasswordSalt.Reverse());
			bi_v = new BigInteger(_account.PasswordVerifier.Reverse());
			bi_B = (bi_v * bi_k + bi_g.modPow(bi_b, bi_N)) % bi_N;

			_client.Send(GetLogonChallenge());
		}

		public IPacket GetLogonChallenge() {
			var result = new AuthPacket(RMSG.AUTH_LOGON_CHALLENGE);
			BinaryWriter w = result.CreateWriter();
			w.Write((byte)0);
			w.Write((byte)0);
			w.Write(bi_B.getBytes().Reverse());
			w.Write((byte)1);
			w.Write(bi_g.getBytes().Reverse());
			w.Write((byte)32);
			w.Write(bi_N.getBytes().Reverse());
			w.Write(bi_s.getBytes().Reverse());
			w.Write(new byte[16]);
			w.Write((byte)0);
			return result;
		}

		private void HandleLogonProof(IPacket packet) {
			BinaryReader gr = packet.CreateReader();

			var bi_A = new BigInteger(gr.ReadBytes(32).Reverse());
			var bi_M1 = new BigInteger(gr.ReadBytes(20).Reverse());

			byte[] u = sha1.ComputeHash(Utility.Concat(bi_A.getBytes().Reverse(), bi_B.getBytes().Reverse()));
			var bi_u = new BigInteger(u.Reverse());

			BigInteger bi_Temp2 = (bi_A * bi_v.modPow(bi_u, bi_N)) % bi_N; // v^u
			BigInteger bi_S = bi_Temp2.modPow(bi_b, bi_N); // (Av^u)^b
			//Console.WriteLine(bi_S.ToHexString());
			byte[] S = bi_S.getBytes().Reverse();
			var S1 = new byte[16];
			var S2 = new byte[16];

			for(int i = 0; i < 16; i++) {
				S1[i] = S[i * 2];
				S2[i] = S[i * 2 + 1];
			}

			var SS_Hash = new byte[40];
			byte[] S1_Hash = sha1.ComputeHash(S1);
			byte[] S2_Hash = sha1.ComputeHash(S2);
			for(int i = 0; i < 20; i++) {
				SS_Hash[i * 2] = S1_Hash[i];
				SS_Hash[i * 2 + 1] = S2_Hash[i];
			}

			_account.SessionKey = (byte[])SS_Hash.Clone();
			_dao.Save(_account);
			_dao.SubmitChanges();

			byte[] N_Hash = sha1.ComputeHash(bi_N.getBytes().Reverse());
			byte[] G_Hash = sha1.ComputeHash(bi_g.getBytes().Reverse());
			for(int i = 0; (i < 20); i++) {
				N_Hash[i] ^= G_Hash[i];
			}

			byte[] UserHash = sha1.ComputeHash(Encoding.UTF8.GetBytes(ClientInfo.AccountName));

			byte[] Temp = Utility.Concat(N_Hash, UserHash);
			Temp = Utility.Concat(Temp, bi_s.getBytes().Reverse());
			Temp = Utility.Concat(Temp, bi_A.getBytes().Reverse());
			Temp = Utility.Concat(Temp, bi_B.getBytes().Reverse());
			Temp = Utility.Concat(Temp, SS_Hash);

			var bi_M1Temp = new BigInteger(sha1.ComputeHash(Temp).Reverse());
			if(bi_M1Temp != bi_M1) {
				_client.Send(GetLogonProof());
				return;
			}

			Temp = Utility.Concat(bi_A.getBytes().Reverse(), bi_M1Temp.getBytes().Reverse());
			Temp = Utility.Concat(Temp, SS_Hash);
			byte[] M2 = sha1.ComputeHash(Temp);

			_client.Send(GetLogonProof(M2));
		}

		public static IPacket GetLogonProof() {
			var result = new AuthPacket(RMSG.AUTH_LOGON_PROOF);
			BinaryWriter w = result.CreateWriter();
			w.Write((byte)4);
			w.Write((byte)3);
			w.Write((byte)0);
			return result;
		}

		public static IPacket GetLogonProof(byte[] M2) {
			var result = new AuthPacket(RMSG.AUTH_LOGON_PROOF);
			BinaryWriter w = result.CreateWriter();
			w.Write((byte)0);
			w.Write(M2);
			w.Write((ushort)0);
			w.Write((uint)0);
			w.Write((uint)0);
			return result;
		}

		private void HandleRealmList() {
			_client.Send(GetRealmList());
		}

		public IPacket GetRealmList() {
			var result = new AuthPacket(RMSG.REALM_LIST);

			BinaryWriter w = result.CreateWriter();
			w.Write(1);
			w.Write((ushort)_realmList.Count);
			foreach(WorldServerInfo info in _realmList) {
				w.Write((byte)info.Type);
				w.Write((byte)(info.Locked ? 1 : 0));
				w.Write((byte)info.Status);
				w.WriteCString(info.Name);
				w.WriteCString(info.Address);
				w.Write(info.Population);
				w.Write(info.CharactersCount);
				w.Write(info.Language);
				w.Write(info.Unk);
			}
			w.Write((ushort)2);
			return result;
		}

		private void HandleXferResume(IPacket packet) {
			AcceptPatch = true;
			BinaryReader gr = packet.CreateReader();
			sendPatch("wow-patch.mpq", gr.ReadInt64());
		}

		private void HandleXferAccept() {
			AcceptPatch = true;
			sendPatch("wow-patch.mpq", 0);
		}

		private void HandleXferCancel() {
			AcceptPatch = false;
		}


		public void sendPatch(string filename, long offset) {
			var th = new Thread(ThreadedSend) {
				IsBackground = true
			};
			th.Start(new PatchInfo { FileName = filename, Offset = offset });
		}

		public void ThreadedSend(object state) {
			var pinfo = (PatchInfo)state;
			try {
				using(var s = (Stream)new FileStream(pinfo.FileName, FileMode.OpenOrCreate)) {
					var buff = new byte[1503];
					s.Seek(pinfo.Offset, SeekOrigin.Begin);
					while(AcceptPatch && s.Position < s.Length) {
						int n = s.Read(buff, 0, 1500);
						_client.Send(GetXferData(buff, 0, n));
					}
				}
			}
			catch {
			}
		}

		public static IPacket GetXferData(byte[] buff, int index, int count) {
			var result = new AuthPacket(RMSG.XFER_DATA);
			BinaryWriter w = result.CreateWriter();
			w.Write(buff, index, count);
			return result;
		}

		#region Nested type: PatchInfo

		public struct PatchInfo {
			public string FileName;
			public long Offset;
		}

		#endregion
	}
}