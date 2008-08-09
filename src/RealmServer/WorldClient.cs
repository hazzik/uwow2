using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hazzik.Net;
using System.Net.Sockets;
using System.IO;
using System.Security.Cryptography;
using Hazzik.Cryptography;
using Hazzik.Helper;
using ICSharpCode.SharpZipLib.Zip.Compression.Streams;

namespace Hazzik {
	public class WorldClient : ClientBase {
		public static byte[] SS;
		ICryptoTransform _decryptor;
		ICryptoTransform _encryptor;
		string _accountName = "ADMIN";

		bool _firstPacket = true;
		uint _seed = (uint)(new Random().Next(0, int.MaxValue));

		public WorldClient(Socket socket)
			: base(socket) {
			ServerPacket sp = new ServerPacket(OpCodes.SMSG_AUTH_CHALLENGE);
			sp.Write(_seed);
			_socket.Send(sp.GetComplete());

			HashAlgorithm hash = new HMACSHA1(new byte[] { 0x38, 0xA7, 0x83, 0x15, 0xF8, 0x92, 0x25, 0x30, 0x71, 0x98, 0x67, 0xB1, 0x8C, 0x4, 0xE2, 0xAA });
			var key = hash.ComputeHash(SS);

			var algo = (SymmetricAlgorithm)new SRP6Wow(key);
			_decryptor = algo.CreateDecryptor();
			_encryptor = algo.CreateEncryptor();
			this.Start();
		}

		private byte[] computeDigest(uint client_seed) {
			var buff = (byte[])null;
			using(BinaryWriter w = new BinaryWriter(new MemoryStream())) {
				w.Write(Encoding.UTF8.GetBytes(_accountName));
				w.Write(0);
				w.Write(client_seed);
				w.Write(_seed);
				w.Write(SS);
				w.Flush();
				buff = (w.BaseStream as MemoryStream).ToArray();
				buff = SHA1.Create().ComputeHash(buff, 0, buff.Length);
			}
			return buff;
		}

		public override void ProcessData(IPacket packet) {
			var size = packet.Size;
			var code = (OpCodes)packet.Code;
			Console.WriteLine("Handle {0}", code);
			if(code == OpCodes.CMSG_AUTH_SESSION) {
				var dataStream = packet.GetStream();
				var r = new BinaryReader(dataStream);
				var version = r.ReadUInt32();
				var unk2 = r.ReadUInt32();
				var accountName = r.ReadCString();
#if WOW3
				var unk = r.ReadUInt32();
#endif
				var clientSeed = r.ReadUInt32();
				var clientDigest = r.ReadBytes(20);

				var addonInfoBlockSize = r.ReadUInt32();
				dataStream = new InflaterInputStream(dataStream);//дальше данные запакованы
				r = new BinaryReader(dataStream);
				try {
					while(true) {
						var addonInfo = new AddonInfo() {
							Name = r.ReadCString(),
							Crc = r.ReadUInt64(),
							Status = r.ReadByte(),
						};
						AddonManager.Instance[addonInfo.Name] = addonInfo;
					}
				} catch(Exception e) {

				}


				byte[] serverDigest = computeDigest(clientSeed);

				for(int i = 0; i < 20; i++) {
					if(serverDigest[i] != clientDigest[i]) {
						throw new Exception();
					}
				}

				using(var w = new BinaryWriter(new NetworkStream(_socket, false))) {
					w.Write((byte)0);
					w.Write((byte)13);
					w.Write((ushort)OpCodes.SMSG_AUTH_RESPONSE);
					w.Write((byte)0x0C);
					w.Write((byte)0xCF);
					w.Write((byte)0xD2);
					w.Write((byte)0x07);
					w.Write((byte)0x00);
					w.Write((byte)0x00);
					w.Write(0);
					w.Write((byte)1); // 0x01 for enabling Burning Crusade Races
				}
			}
		}


		public override IPacket ReadPacket() {
			Stream dataStream = this.GetStream();
			Stream headStream = _firstPacket ? dataStream : new CryptoStream(dataStream, _decryptor, CryptoStreamMode.Read);

			var data = new byte[6];
			headStream.Read(data, 0, 6);
			int len = data[0] << 8 | data[1];
			int code = data[3] << 8 | data[2];

			using(var reader = new BinaryReader(dataStream)) {
				return new WorldPacketIn(code, reader.ReadBytes(len - 4));
			}
		}
	}
}
