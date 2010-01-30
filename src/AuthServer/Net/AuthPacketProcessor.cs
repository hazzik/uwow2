using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using Hazzik.Data;
using Hazzik.IO;

namespace Hazzik.Net {
	public class AuthPacketProcessor : IPacketProcessor {
		private static readonly BigInteger bi_g = 7;
		private static readonly BigInteger bi_k = 3;

		private static readonly BigInteger bi_N =
			new BigInteger("894B645E89E1535BBDAD5B8B290650530801B18EBFBF5E8FAB3C82872A3E9BB7", 16);

		private static readonly SHA1 sha1 = SHA1.Create();
		private readonly IPacketSender _client;
		private readonly IList<WorldServerInfo> _realmList = new List<WorldServerInfo>();
		private readonly BigInteger bi_b = BigInteger.genPseudoPrime(160, 5, Utility.Seed);
		private Account _account;
		private BigInteger bi_B;
		private BigInteger bi_s = BigInteger.genPseudoPrime(256, 5, Utility.Seed);
		private BigInteger bi_v;

		public AuthPacketProcessor(IPacketSender client) {
			_client = client;
			_realmList = new List<WorldServerInfo> {
				new WorldServerInfo {
					Type = 0,
					Locked = false,
					Status = 0,
					Name = "TestRealm",
					Address = "77.222.102.234:3725",
					CharactersCount = 1,
					Language = 8,
					Population = 1,
					Unk = 0
				},
			};
		}

		private ClientInfo ClientInfo { get; set; }

		private bool AcceptPatch { get; set; }

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

			var repository1 = IoC.Resolve<IAccountRepository>();
			_account = repository1.FindByName(accountName);
			if(_account == null) {
				_account = new Account { Name = accountName };
				_account.SetPassword(accountName);
				repository1.Save(_account);
				repository1.SubmitChanges();
			}

			bi_s = new BigInteger(_account.PasswordSalt.Reverse());
			bi_v = new BigInteger(_account.PasswordVerifier.Reverse());
			bi_B = (bi_v * bi_k + bi_g.modPow(bi_b, bi_N)) % bi_N;

			_client.Send(GetLogonChallenge());
		}

		private IPacket GetLogonChallenge() {
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

			byte[] u = H(bi_A.getBytes().Reverse().Concat(bi_B.getBytes().Reverse()));
			var bi_u = new BigInteger(u.Reverse());

			BigInteger bi_Temp2 = (bi_A * bi_v.modPow(bi_u, bi_N)) % bi_N; // v^u
			BigInteger bi_S = bi_Temp2.modPow(bi_b, bi_N); // (Av^u)^b

			byte[] S = bi_S.getBytes().Reverse();
			var S1 = new byte[16];
			var S2 = new byte[16];

			for(int i = 0; i < 16; i++) {
				S1[i] = S[i * 2];
				S2[i] = S[i * 2 + 1];
			}

			var SS_Hash = new byte[40];
			byte[] S1_Hash = H(S1);
			byte[] S2_Hash = H(S2);
			for(int i = 0; i < 20; i++) {
				SS_Hash[i * 2] = S1_Hash[i];
				SS_Hash[i * 2 + 1] = S2_Hash[i];
			}

			_account.SessionKey = (byte[])SS_Hash.Clone();

			var accountRepository = IoC.Resolve<IAccountRepository>();
			accountRepository.Save(_account);
			accountRepository.SubmitChanges();

			byte[] N_Hash = H(bi_N.getBytes().Reverse());
			byte[] G_Hash = H(bi_g.getBytes().Reverse());
			for(int i = 0; (i < 20); i++) {
				N_Hash[i] ^= G_Hash[i];
			}

			byte[] userHash = H(Encoding.UTF8.GetBytes(ClientInfo.AccountName));

			IEnumerable<byte> temp = N_Hash
				.Concat(userHash)
				.Concat(bi_s.getBytes().Reverse())
				.Concat(bi_A.getBytes().Reverse())
				.Concat(bi_B.getBytes().Reverse())
				.Concat(SS_Hash);

			var biM1Temp = new BigInteger(H(temp).Reverse());
			if(biM1Temp != bi_M1) {
				_client.Send(GetLogonProof());
				return;
			}

			temp = bi_A.getBytes().Reverse()
				.Concat(biM1Temp.getBytes().Reverse());
			temp = temp.Concat(SS_Hash);
			byte[] M2 = H(temp);

			_client.Send(GetLogonProof(M2));
		}

		private static byte[] H(IEnumerable<byte> buffer) {
			return sha1.ComputeHash(buffer.ToArray());
		}

		private static IPacket GetLogonProof() {
			var result = new AuthPacket(RMSG.AUTH_LOGON_PROOF);
			BinaryWriter w = result.CreateWriter();
			w.Write((byte)4);
			w.Write((byte)3);
			w.Write((byte)0);
			return result;
		}

		private static IPacket GetLogonProof(byte[] M2) {
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

		private IPacket GetRealmList() {
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
			SendPatch("wow-patch.mpq", gr.ReadInt64());
		}

		private void HandleXferAccept() {
			AcceptPatch = true;
			SendPatch("wow-patch.mpq", 0);
		}

		private void HandleXferCancel() {
			AcceptPatch = false;
		}


		private void SendPatch(string filename, long offset) {
			var th = new Thread(SendPatchBackground) {
				IsBackground = true
			};
			th.Start(new PatchInfo { FileName = filename, Offset = offset });
		}

		private void SendPatchBackground(object state) {
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

		private static IPacket GetXferData(byte[] buff, int index, int count) {
			var result = new AuthPacket(RMSG.XFER_DATA);
			BinaryWriter w = result.CreateWriter();
			w.Write(buff, index, count);
			return result;
		}

		#region Nested type: PatchInfo

		private struct PatchInfo {
			public string FileName { get; set; }
			public long Offset { get; set; }
		}

		#endregion
	}
}