using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Linq;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using Hazzik.Helper;
using Hazzik.Net;

namespace Hazzik.Net {
	public class AuthClient : ClientBase, IClient {

		static BigInteger bi_N = new BigInteger("894B645E89E1535BBDAD5B8B290650530801B18EBFBF5E8FAB3C82872A3E9BB7", 16);
		static BigInteger bi_g = 7;
		static BigInteger bi_k = 3;
		static SHA1 sha1 = SHA1.Create();

		private BigInteger bi_b = BigInteger.genPseudoPrime(160, 5, Utility.seed2);
		private BigInteger bi_v;
		private BigInteger bi_B;
		private BigInteger bi_s = BigInteger.genPseudoPrime(256, 5, Utility.seed2);
		private Account _account;
		private AuthServer _server;

		public AuthClient(AuthServer server, Socket client) :
			base(client) {
			_serverList.Add(new WorldServerInfo {
				Type = 0,
				Locked = 0,
				Status = 0,
				Name = "TestRealm",
				Address = "127.0.0.1:3725",
				CharactersCount = 1,
				Language = (byte)8,
				Population = 1,
				Unk = 0
			});
			_server = server;
			Start();
		}

		public override void ProcessData(IPacket packet) {
			var code = (RMSG)packet.Code;

			switch(code) {
			case RMSG.AUTH_LOGON_CHALLENGE:
			case RMSG.AUTH_LOGON_RECODE_CHALLENGE:
				HandleLogonChallenge(packet);
				break;

			case RMSG.AUTH_LOGON_PROOF:
			case RMSG.AUTH_LOGON_RECODE_PROOF:
				HandleLogonProof(packet);
				break;

			case RMSG.REALM_LIST:
				HandleRealmList(packet);
				break;

			case RMSG.XFER_ACCEPT:
				HandleXferAccept(packet);
				break;

			case RMSG.XFER_RESUME:
				HandleXferResume(packet);
				break;

			case RMSG.XFER_CANCEL:
				HandleXferCancel(packet);
				break;

			default:
				Console.WriteLine("Receive unknown command {0}", code);
				break;
			}
		}

		private List<WorldServerInfo> _serverList = new List<WorldServerInfo>();

		public bool AcceptPatch { get; set; }

		private void sendPatch(string filename, long offset) {
			Thread th = new Thread(ThreadedSend) {
				IsBackground = true
			};
			th.Start(new PatchInfo { FileName = filename, Offset = offset });
		}

		struct PatchInfo {
			public string FileName;
			public long Offset;
		}
		private void ThreadedSend(object state) {
			var pinfo = (PatchInfo)state;
			try {
				using(var s = (Stream)new FileStream(pinfo.FileName, FileMode.OpenOrCreate)) {
					byte[] buff = new byte[1503];
					s.Seek(pinfo.Offset, SeekOrigin.Begin);
					while(this.AcceptPatch && s.Position < s.Length) {
						var p = new AuthPacket(RMSG.XFER_DATA);
						var w = p.CreateWriter();
						var n = s.Read(buff, 0, 1500);
						w.Write(buff, 0, n);
						this.SendPacket(p);
					}
				}
			}
			catch {
			}
		}

		ClientInfo _clientInfo;
		public void HandleLogonChallenge(IPacket packet) {
			var gr = packet.CreateReader();
			var tag = gr.ReadCString();
			var verMajor = (int)gr.ReadByte();
			var verMinor = (int)gr.ReadByte();
			var verBuild = (int)gr.ReadByte();
			var verRevis = (int)gr.ReadUInt16();
			var platform = gr.ReadCString();
			var os = gr.ReadCString();
			var locale = Encoding.UTF8.GetString(gr.ReadBytes(4).Reverse());
			var timezone = gr.ReadInt32();
			var ip = new IPAddress(gr.ReadBytes(4));
			var accountName = gr.ReadString();

			_clientInfo = new ClientInfo {
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

			_account = Account.GetByName(accountName);
			if(_account == null) {
				_account = Account.Create(accountName);
				_account.SetPassword(accountName);
				_account.Save();
			}

			bi_s = new BigInteger(_account.PasswordSalt.Reverse());
			bi_v = new BigInteger(_account.PasswordVerifier.Reverse());
			bi_B = (bi_v * bi_k + bi_g.modPow(bi_b, bi_N)) % bi_N;

			#region sending reply to client
			var p = new AuthPacket((int)RMSG.AUTH_LOGON_CHALLENGE);
			var w = p.CreateWriter();
			{
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
			}
			this.SendPacket(p);
			#endregion
		}

		public void HandleLogonProof(IPacket packet) {
			var gr = packet.CreateReader();

			BigInteger bi_A = new BigInteger(gr.ReadBytes(32).Reverse());
			BigInteger bi_M1 = new BigInteger(gr.ReadBytes(20).Reverse());
			Console.WriteLine(bi_A.ToHexString());
			Console.WriteLine(bi_M1.ToHexString());

			byte[] u = sha1.ComputeHash(Utility.Concat(bi_A.getBytes().Reverse(), bi_B.getBytes().Reverse()));
			BigInteger bi_u = new BigInteger(u.Reverse());

			BigInteger bi_Temp2 = (bi_A * bi_v.modPow(bi_u, bi_N)) % bi_N;			// v^u
			BigInteger bi_S = bi_Temp2.modPow(bi_b, bi_N);	// (Av^u)^b
			Console.WriteLine(bi_S.ToHexString());
			byte[] S = bi_S.getBytes().Reverse();
			byte[] S1 = new byte[16];
			byte[] S2 = new byte[16];

			for(int i = 0; i < 16; i++) {
				S1[i] = S[i * 2];
				S2[i] = S[i * 2 + 1];
			}

			byte[] SS_Hash = new byte[40];
			byte[] S1_Hash = sha1.ComputeHash(S1);
			byte[] S2_Hash = sha1.ComputeHash(S2);
			for(int i = 0; i < 20; i++) {
				SS_Hash[i * 2] = S1_Hash[i];
				SS_Hash[i * 2 + 1] = S2_Hash[i];
			}

			_account.SessionKey = (byte[])SS_Hash.Clone();
			_account.Save();

			byte[] N_Hash = sha1.ComputeHash(bi_N.getBytes().Reverse());
			byte[] G_Hash = sha1.ComputeHash(bi_g.getBytes().Reverse());
			for(int i = 0; (i < 20); i++) {
				N_Hash[i] ^= G_Hash[i];
			}

			byte[] UserHash = sha1.ComputeHash(Encoding.UTF8.GetBytes(_clientInfo.AccountName));

			byte[] Temp = Utility.Concat(N_Hash, UserHash);
			Temp = Utility.Concat(Temp, bi_s.getBytes().Reverse());
			Temp = Utility.Concat(Temp, bi_A.getBytes().Reverse());
			Temp = Utility.Concat(Temp, bi_B.getBytes().Reverse());
			Temp = Utility.Concat(Temp, SS_Hash);

			BigInteger bi_M1Temp = new BigInteger(sha1.ComputeHash(Temp).Reverse());
			if(bi_M1Temp != bi_M1) {
				var p = new AuthPacket(RMSG.AUTH_LOGON_PROOF);
				var w = p.CreateWriter();
				w.Write((byte)4);
				w.Write((byte)3);
				w.Write((byte)0);

				this.SendPacket(p);
				return;
			}

			Temp = Utility.Concat(bi_A.getBytes().Reverse(), bi_M1Temp.getBytes().Reverse());
			Temp = Utility.Concat(Temp, SS_Hash);
			byte[] M2 = sha1.ComputeHash(Temp);

			#region Sending reply to client
			{
				var p = new AuthPacket(RMSG.AUTH_LOGON_PROOF);
				var w = p.CreateWriter();
				w.Write((byte)0);
				w.Write(M2);
				w.Write((ushort)0);
				w.Write((uint)0);
				w.Write((uint)0);
				this.SendPacket(p);
			}
			#endregion
		}

		public void HandleRealmList(IPacket packet) {
			var gr = packet.CreateReader();

			var p = new AuthPacket(RMSG.REALM_LIST);
			var w = p.CreateWriter();
			{
				w.Write(1);
				w.Write((ushort)_serverList.Count);
				foreach(var info in _serverList) {
					/*
					 * 0 = normal
					 * 1 = pvp
					 * 6 = rp
					 * 8 = pvprp
					 */
					w.Write(info.Type);
					/*
					 * 1 = locked
					 */
					w.Write(info.Locked);
					/*
					 * 0 = green realmname
					 * 1 = red realmname
					 * 2 = offline

					 * 32 = recomended(blue)
					 * 64 = recomended(green)
					 * 128 = full(red)
					 */
					w.Write(info.Status);
					w.WriteCString(info.Name);
					w.WriteCString(info.Address);
					w.Write(info.Population);
					w.Write(info.CharactersCount);
					w.Write(info.Language);
					w.Write(info.Unk);
				}
				w.Write((ushort)2);
			}
			this.SendPacket(p);
		}

		public void HandleXferAccept(IPacket packet) {
			this.AcceptPatch = true;
			sendPatch("wow-patch.mpq", 0);
		}

		public void HandleXferResume(IPacket packet) {
			this.AcceptPatch = true;
			var gr = packet.CreateReader();
			sendPatch("wow-patch.mpq", gr.ReadInt64());
		}

		public void HandleXferCancel(IPacket packet) {
			this.AcceptPatch = false;
		}

		public override IPacket ReadPacket() {
			using(var reader = new BinaryReader(this.GetStream())) {
				var code = (RMSG)reader.ReadByte();
				var size = 0;
				switch(code) {
				case RMSG.AUTH_LOGON_CHALLENGE:
				case RMSG.AUTH_LOGON_RECODE_CHALLENGE:
					var unk = reader.ReadByte();
					size = reader.ReadUInt16();
					break;
				case RMSG.AUTH_LOGON_PROOF:
					size = 0x4A;
					break;
				case RMSG.AUTH_LOGON_RECODE_PROOF:
					size = 0x39;
					break;
				case RMSG.REALM_LIST:
					size = 0x04;
					break;
				case RMSG.XFER_RESUME:
					size = 0x08;
					break;
				case RMSG.XFER_ACCEPT:
				case RMSG.XFER_CANCEL:
				default:
					size = 0x00;
					break;
				}
				return new AuthPacket(code, reader.ReadBytes(size));
			}
		}

		public override void SendPacket(IPacket packet) {
			var data = this.GetStream();
			var head = data;

			packet.WriteHead(head);
			packet.WriteBody(data);
		}
	}
}
