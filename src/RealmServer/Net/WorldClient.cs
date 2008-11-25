using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using Hazzik.Cryptography;
using ICSharpCode.SharpZipLib.Zip.Compression.Streams;

namespace Hazzik.Net {
	public class WorldClient : ClientBase {
		public Account Account { get; set; }
		private WorldServer _server;

		private ICryptoTransform _decryptor;
		private ICryptoTransform _encryptor;

		private bool _firstPacket = true;
		private uint _seed = (uint)(new Random().Next(0, int.MaxValue));

		public WorldClient(WorldServer server, Socket socket)
			: base(socket) {
			var p = new WorldPacket(WMSG.SMSG_AUTH_CHALLENGE);
			var w = p.CreateWriter();
			w.Write(_seed);
			Send(p);

			_server = server;
			Start();
		}

		private byte[] computeDigest(uint client_seed) {
			var buff = (byte[])null;
			using(var w = new BinaryWriter(new MemoryStream())) {
				w.Write(Encoding.UTF8.GetBytes(Account.Name));
				w.Write(0);
				w.Write(client_seed);
				w.Write(_seed);
				w.Write(Account.SessionKey);
				w.Flush();
				buff = (w.BaseStream as MemoryStream).ToArray();
				buff = SHA1.Create().ComputeHash(buff, 0, buff.Length);
			}
			return buff;
		}

		public override void ProcessData(IPacket packet) {
			var size = packet.Size;
			var code = (WMSG)packet.Code;
			Console.WriteLine("Handle {0}", code);
			if(code == WMSG.CMSG_AUTH_SESSION) {
				_firstPacket = false;

				var dataStream = packet.GetStream();
				var r = packet.CreateReader();
				var version = r.ReadUInt32();
				var unk2 = r.ReadUInt32();
				var accountName = r.ReadCString();
#if WOW3
				var unk = r.ReadUInt32();
#endif
				var clientSeed = r.ReadUInt32();
				var clientDigest = r.ReadBytes(20);

				Account = Account.GetByName(accountName);

				var hash =
					(HashAlgorithm)
					new HMACSHA1(new byte[]
					             { 0x38, 0xA7, 0x83, 0x15, 0xF8, 0x92, 0x25, 0x30, 0x71, 0x98, 0x67, 0xB1, 0x8C, 0x4, 0xE2, 0xAA });
				var key = hash.ComputeHash(Account.SessionKey);
				var algo = (SymmetricAlgorithm)new SRP6Wow(key);
				_decryptor = algo.CreateDecryptor();
				_encryptor = algo.CreateEncryptor();

				var serverDigest = computeDigest(clientSeed);

				for(int i = 0; i < 20; i++) {
					if(serverDigest[i] != clientDigest[i]) {
						throw new Exception();
					}
				}

				var p = new WorldPacket(WMSG.SMSG_AUTH_RESPONSE);
				var w = p.CreateWriter();
				w.Write((byte)0x0C);
				w.Write((uint)0);
				w.Write((byte)0);
				w.Write((uint)0);
				w.Write((byte)Account.Expansion);
				Send(p);

				var addonInfoBlockSize = r.ReadUInt32();
				dataStream = new InflaterInputStream(dataStream); //дальше данные запакованы
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
				}
				catch(Exception e) {
				}

				p = new WorldPacket(WMSG.SMSG_ADDON_INFO);
				w = p.CreateWriter();
				foreach(var item in AddonManager.Instance.AddonInfos) {
					w.Write((ulong)0x0102);
				}
				w.Flush();
				Send(p);
				return;
			}
			if(code == WMSG.CMSG_PING) {
				var r = packet.CreateReader();
				var p = new WorldPacket(WMSG.SMSG_PONG);
				var w = p.CreateWriter();
				w.Write(r.ReadUInt32());
				Send(p);
				return;
			}
			_server.Handler.Handle(this, packet);
		}

		public override IPacket ReadPacket() {
			var data = GetStream();
			var head = _firstPacket ? data : new CryptoStream(data, _decryptor, CryptoStreamMode.Read);

			int size = ReadSize(head);
			int code = ReadCode(head);

			using(var reader = new BinaryReader(data)) {
				return new WorldPacket((WMSG)code, reader.ReadBytes(size - 4));
			}
		}

		public override void Send(IPacket packet) {
			var data = GetStream();
			var head = _firstPacket ? data : new CryptoStream(data, _encryptor, CryptoStreamMode.Write);
			WriteSize(head, packet.Size + 2);
			WriteCode(head, packet);
			packet.WriteBody(data);
		}

		private static int ReadSize(Stream stream) {
			int len = stream.ReadByte();
			if((len & 0x80) != 0x00) {
				len &= 0x7f;
				len = (len << 0x08) | stream.ReadByte();
			}
			len = (len << 0x08) | stream.ReadByte();
			return len;
		}

		private static int ReadCode(Stream stream) {
			int code = 0;
			code |= stream.ReadByte();
			code |= stream.ReadByte() << 0x08;
			code |= stream.ReadByte() << 0x10;
			code |= stream.ReadByte() << 0x18;
			return code;
		}

		private static void WriteSize(Stream stream, int size) {
			if(size > short.MaxValue) {
				stream.WriteByte((byte)(size >> 0x10));
			}
			stream.WriteByte((byte)(size >> 0x08));
			stream.WriteByte((byte)(size));
		}

		private static void WriteCode(Stream stream, IPacket packet) {
			stream.WriteByte((byte)(packet.Code));
			stream.WriteByte((byte)(packet.Code >> 0x08));
		}
	}
}
