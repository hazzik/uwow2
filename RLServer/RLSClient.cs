using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;
using System.Security.Cryptography;
using Helper;
using System.IO;
using System.Net;
using System.Collections;
using System.Runtime.Serialization;
using System.Runtime.InteropServices;
using UWoW.Net;

namespace UWoW {
	
	#region stuff
	public enum eAuthResults {
		REALM_AUTH_SUCCESS = 0,
		REALM_AUTH_FAILURE = 0x01,                                ///< Unable to connect
		REALM_AUTH_UNKNOWN1 = 0x02,                               ///< Unable to connect
		REALM_AUTH_ACCOUNT_BANNED = 0x03,                         ///< This <game> account has been closed and is no longer available for use. Please go to <site>/banned.html for further information.
		REALM_AUTH_NO_MATCH = 0x04,                               ///< The information you have entered is not valid. Please check the spelling of the account name and password. If you need help in retrieving a lost or stolen password, see <site> for more information
		REALM_AUTH_UNKNOWN2 = 0x05,                               ///< The information you have entered is not valid. Please check the spelling of the account name and password. If you need help in retrieving a lost or stolen password, see <site> for more information
		REALM_AUTH_ACCOUNT_IN_USE = 0x06,                         ///< This account is already logged into <game>. Please check the spelling and try again.
		REALM_AUTH_PREPAID_TIME_LIMIT = 0x07,                     ///< You have used up your prepaid time for this account. Please purchase more to continue playing
		REALM_AUTH_SERVER_FULL = 0x08,                            ///< Could not log in to <game> at this time. Please try again later.
		REALM_AUTH_WRONG_BUILD_NUMBER = 0x09,                     ///< Unable to validate game version. This may be caused by file corruption or interference of another program. Please visit <site> for more information and possible solutions to this issue.
		REALM_AUTH_UPDATE_CLIENT = 0x0a,                          ///< Downloading
		REALM_AUTH_UNKNOWN3 = 0x0b,                               ///< Unable to connect
		REALM_AUTH_ACCOUNT_FREEZED = 0x0c,                        ///< This <game> account has been temporarily suspended. Please go to <site>/banned.html for further information
		REALM_AUTH_UNKNOWN4 = 0x0d,                               ///< Unable to connect
		REALM_AUTH_UNKNOWN5 = 0x0e,                               ///< Connected.
		REALM_AUTH_PARENTAL_CONTROL = 0x0f                        ///< Access to this account has been blocked by parental controls. Your settings may be changed in your account preferences at <site>
	};

	public class ServerInfo {
		byte _type;
		public byte Type {
			get { return _type; }
			set { _type = value; }
		}

		byte _locked;
		public byte Locked {
			get { return _locked; }
			set { _locked = value; }
		}

		byte _status;
		public byte Status {
			get { return _status; }
			set { _status = value; }
		}

		string _name;
		public string Name {
			get { return _name; }
			set { _name = value; }
		}

		string _address;
		public string Address {
			get { return _address; }
			set { _address = value; }
		}

		float _population;
		public float Population {
			get { return _population; }
			set { _population = value; }
		}

		byte _charactersCount;
		public byte CharactersCount {
			get { return _charactersCount; }
			set { _charactersCount = value; }
		}

		byte _language;
		public byte Language {
			get { return _language; }
			set { _language = value; }
		}

		byte _unk;
		public byte Unk {
			get { return _unk; }
			set { _unk = value; }
		}
	}

	public class Account {
		static BigInteger bi_N = new BigInteger("B79B3E2A87823CAB8F5EBFBF8EB10108535006298B5BADBD5B53E1895E644B89", 16);
		static BigInteger bi_g = 7;
		static BigInteger bi_k = 3;
		static SHA1 sha1 = new SHA1Managed();

		private string _name;

		private byte[] _s;
		private byte[] _v;
		public void SetPassword(string password) {
			var p = (_name + ":" + password).ToUpper();
			var pHash = sha1.ComputeHash(Encoding.UTF8.GetBytes(p));
			var x = sha1.ComputeHash(Utility.Concat(_s, pHash));
			var bi_x = new BigInteger(x.Reverse());
			var bi_v = bi_g.modPow(bi_x, bi_N);
		}
	}

	#endregion

	public class RLSClient : AClient {
		private enum Tag {
			Client = 0x00576F57, //"WoW"
			Server = 0x00537276, //"Srv"
		}

		private Socket _socket;

		static BigInteger bi_N = new BigInteger("B79B3E2A87823CAB8F5EBFBF8EB10108535006298B5BADBD5B53E1895E644B89", 16);
		static BigInteger bi_Nr = new BigInteger(bi_N.getBytes().Reverse());
		static BigInteger bi_g = 7;
		static BigInteger bi_k = 3;
		static MD5 md5 = new MD5CryptoServiceProvider();
		static SHA1 sha1 = new SHA1Managed();

		private BigInteger bi_b = BigInteger.genPseudoPrime(160, 5, Utility.seed2);
		private BigInteger bi_v;
		private BigInteger bi_B;
		private BigInteger bi_s = BigInteger.genPseudoPrime(256, 5, Utility.seed2);

		private Account myAccount;
		public static int s1;
		private byte[] userName;

		public RLSClient(Socket a) {
			_socket = a;
			_serverList.Add(new ServerInfo {
				Type = 0,
				Locked = 0,
				Status = 0,
				Name = "TestRealm",
				Address = "127.0.0.1:1234",
				CharactersCount = 1,
				Language = (byte)0,
				Population = 1,
				Unk = 0
			});
			Start();
		}

		public override void Start() {
			try {
				receivedBuff = new byte[0xFFFF];
				int n = 0;
				while((n = _socket.Receive(receivedBuff, receivedBuff.Length, SocketFlags.None)) > 0)
					PocessData(receivedBuff, 0, n);
			} catch(SocketException) {
			} catch(Exception e) {
				Console.WriteLine(e.Message);
				Console.WriteLine(e.StackTrace);
			}
			_socket.Close();
		}

		public override void PocessData(byte[] data, int offset, int length) {
			MemoryStream ms = new MemoryStream(data, offset, length, false);
			IGenReader gr = new GenericReader(ms);
			using(TextWriter tw = new StreamWriter("dump1.txt", true)) {
				Utility.View(tw, data, offset, length);
			}

			byte opcode = gr.ReadByte();
			switch(opcode) {
			case 0:
			case 2:
				HandleLogonChallenge(gr);
				break;

			case 1:
			case 3:
				HandleLogonProof(gr);
				break;

			case 4:
				break;

			case 0x10:
				HandleRealmList(gr);
				break;

			case 0x32:
				HandleXferAccept(gr);
				break;

			case 0x33:
				HandleXferResume(gr);
				break;

			case 0x34:
				HandleXferCancel(gr);

				break;
			default:
				Console.WriteLine("Receive unknown command {0}", data[0]);
				break;
			}
		}

		private List<ServerInfo> _serverList = new List<ServerInfo>();

		private bool canSendPatch = true;
		private byte[] patchAvailable {
			get {
				using(var s = (Stream)new FileStream("wow-patch.mpq", FileMode.OpenOrCreate)) {
					MD5 md5 = new MD5CryptoServiceProvider();

					using(var ms = new MemoryStream()) {
						IGenWriter w = new GenericWriter(ms);
						w.Write((byte)0x30); // send patch :)
						w.Write("Patch");
						w.Write(s.Length);
						w.Write(md5.ComputeHash(s));
						w.Close();
						return ms.ToArray();
					}
				}

			}
		}

		private void sendPatch(string filename, long offset) {
			using(var s = (Stream)new FileStream(filename, FileMode.OpenOrCreate)) {
				byte[] buff = new byte[1503];

				s.Seek(offset, SeekOrigin.Begin);
				while(canSendPatch && s.Position < s.Length) {
					int n = s.Read(buff, 3, 1500);
					buff[0] = 0x31;
					buff[1] = (byte)n;
					buff[2] = (byte)(n >> 8);
					_socket.Send(buff, n + 3, SocketFlags.None);
				}
			}
		}

		//private void process
		public void HandleLogonChallenge(IGenReader gr) {
			//0000000000: 00 06 2B 00 57 6F 57 00 02 | 01 00 24 1A 36 38 78  ..+.WoW....$.68x
			//0000000010: 00 6E 69 57 00 42 47 6E 65 | 2C 01 00 00 7F 00 00  .niW.BGne,.....
			//0000000020: 01 0D 56 45 4E 47 45 41 4E | 43 45 31 39 37 37 --  ..VENGEANCE1977

			uint ver = gr.ReadByte();//protocol version?
			uint length2 = gr.ReadUInt16(); //
			Tag tag = (Tag)gr.ReadUInt32(); // clienttag
			switch(tag) {
			case Tag.Client:
				Console.WriteLine("Client connected");
				break;
			case Tag.Server:
				Console.WriteLine("World Server connected...");
				break;
			default:
				break;
			}
			int ver_major = gr.ReadByte();
			int ver_minor = gr.ReadByte();
			int ver_build = gr.ReadByte();
			int ver_revis = gr.ReadUInt16();

			string r = gr.ReadString0();

			Console.WriteLine(new string(gr.ReadChars(4)));

			gr.BaseStream.Seek(11, SeekOrigin.Begin);
			ushort client_version = gr.ReadUInt16();
			if(client_version < 5595) {
				_socket.Send(new byte[] { 1, 2 });
				return;
			}
			gr.BaseStream.Seek(33, SeekOrigin.Begin);
			userName = gr.ReadBytes(gr.ReadByte());

			//this.myAccount.client_build = client_version;

			Random rand = new Random();

			string Temp_str = "ADMIN:WPDIR4PA";
			Temp_str = Temp_str.ToUpper();

			byte[] TempHash = sha1.ComputeHash(Encoding.UTF8.GetBytes(Temp_str));

			byte[] x = sha1.ComputeHash(Utility.Concat(bi_s.getBytes().Reverse(), TempHash));
			BigInteger bi_x = new BigInteger(x.Reverse());
			bi_v = bi_g.modPow(bi_x, bi_N);

			bi_B = (bi_v * bi_k + bi_g.modPow(bi_b, bi_N)) % bi_N;

			#region sending reply to client
			using(var ms1 = new MemoryStream()) {
				var w = new BinaryWriter(ms1);
				w.Write((byte)0);
				w.Write((byte)0);
				w.Write((byte)0);
				w.Write(bi_B.getBytes().Reverse());
				w.Write((byte)1);
				w.Write(bi_g.getBytes().Reverse());
				w.Write((byte)32);
				w.Write(bi_N.getBytes().Reverse());
				w.Write(bi_s.getBytes().Reverse());
				w.Write(new byte[17]);

				_socket.Send(ms1.ToArray());
			}
			#endregion
		}

		public void HandleLogonProof(IGenReader gr) {
			gr.BaseStream.Seek(1, SeekOrigin.Begin);

			byte[] A = gr.ReadBytes(32);
			byte[] M1 = gr.ReadBytes(20);

			BigInteger bi_A = new BigInteger(A.Reverse());

			byte[] u = sha1.ComputeHash(Utility.Concat(A, bi_B.getBytes().Reverse()));
			BigInteger bi_u = new BigInteger(u.Reverse());

			BigInteger bi_Temp2 = (bi_A * bi_v.modPow(bi_u, bi_N)) % bi_N;			// v^u
			BigInteger bi_S = bi_Temp2.modPow(bi_b, bi_N);				// (Av^u)^b

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

			byte[] N_Hash = sha1.ComputeHash(bi_N.getBytes().Reverse());
			byte[] G_Hash = sha1.ComputeHash(bi_g.getBytes());
			for(int i = 0; (i < 20); i++) {
				N_Hash[i] ^= G_Hash[i];
			}

			byte[] UserHash = sha1.ComputeHash(userName);


			byte[] Temp = Utility.Concat(N_Hash, UserHash);
			Temp = Utility.Concat(Temp, bi_s.getBytes().Reverse());
			Temp = Utility.Concat(Temp, A);
			Temp = Utility.Concat(Temp, bi_B.getBytes().Reverse());
			Temp = Utility.Concat(Temp, SS_Hash);

			byte[] M1Temp = sha1.ComputeHash(Temp);

			BigInteger bi_M1Temp = new BigInteger(M1Temp);
			BigInteger bi_M1 = new BigInteger(M1);
			if(bi_M1Temp != bi_M1) {
				_socket.Send(new byte[] { 1, 4, 3, 0 });
				return;
			}

			Temp = Utility.Concat(A, M1Temp);
			Temp = Utility.Concat(Temp, SS_Hash);
			byte[] M2 = sha1.ComputeHash(Temp);

			#region Sending reply to client
			using(var ms1 = new MemoryStream()) {
				var w = new BinaryWriter(ms1);
				w.Write((byte)1);
				w.Write((byte)0);
				w.Write(M2);
				w.Write((ushort)0);
				w.Write((uint)0);
				w.Write((uint)0);

				_socket.Send(ms1.ToArray());
			}
			#endregion
		}

		public void HandleRealmList(IGenReader gr) {
			using(var ms = new MemoryStream()) {

				IGenWriter gw = new GenericWriter(ms);

				gw.Write((byte)0x10);
				gw.Write((ushort)0); // packet size
				gw.Write(0);

				gw.Write((ushort)_serverList.Count);
				foreach(var info in _serverList) {
					/*
					 * 0 = normal
					 * 1 = pvp
					 * 6 = rp
					 * 8 = pvprp
					 */
					gw.Write(info.Type);
					/*
					 * 1 = locked
					 */
					gw.Write(info.Locked);
					/*
					 * 0 = green realmname
					 * 1 = red realmname
					 * 2 = offline

					 * 32 = recomended(blue)
					 * 64 = recomended(green)
					 * 128 = full(red)
					 */
					gw.Write(info.Status);
					gw.WriteString0(info.Name);
					gw.WriteString0(info.Address);
					gw.Write(info.Population);
					gw.Write(info.CharactersCount);
					gw.Write(info.Language);
					gw.Write(info.Unk);
				}

				gw.Write((ushort)2);

				gw.BaseStream.Seek(1, SeekOrigin.Begin);
				gw.Write((ushort)(gw.BaseStream.Length - 3));

				_socket.Send(ms.ToArray());
			}
		}

		public void HandleXferAccept(IGenReader gr) {
			//send full patch:)
			sendPatch("wow-patch.mpq", 0);
		}

		public void HandleXferResume(IGenReader gr) {
			// send partial patch:)
			sendPatch("wow-patch.mpq", gr.ReadInt64());
		}

		public void HandleXferCancel(IGenReader gr) {
			canSendPatch = false;
		}
	}
}
