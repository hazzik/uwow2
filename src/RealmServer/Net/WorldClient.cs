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
using System.Net;
using System.Runtime.InteropServices;

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
			var w = p.GetWriter();
			w.Write(_seed);
			this.WritePacket(p);

			this._server = server;
			this.Start();
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
				var r = packet.GetReader();
				var version = r.ReadUInt32();
				var unk2 = r.ReadUInt32();
				var accountName = r.ReadCString();
#if WOW3
				var unk = r.ReadUInt32();
#endif
				var clientSeed = r.ReadUInt32();
				var clientDigest = r.ReadBytes(20);

				Account = AccountManager.Instance.GetAccountByName(accountName);

				var hash = (HashAlgorithm)new HMACSHA1(new byte[] { 0x38, 0xA7, 0x83, 0x15, 0xF8, 0x92, 0x25, 0x30, 0x71, 0x98, 0x67, 0xB1, 0x8C, 0x4, 0xE2, 0xAA });
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
				var w = p.GetWriter();
				w.Write((byte)0x0C);
				w.Write((uint)0);
				w.Write((byte)0);
				w.Write((uint)0);
				w.Write((byte)Account.Expansion);
				this.WritePacket(p);

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

				p = new WorldPacket(WMSG.SMSG_ADDON_INFO);
				w = p.GetWriter();
				foreach(var item in AddonManager.Instance.AddonInfos) {
					w.Write((ulong)0x0102);
				}
				w.Flush();
				this.WritePacket(p);
				return;
			}
			if(code == WMSG.CMSG_PING) {
				var r = packet.GetReader();
				var p = new WorldPacket(WMSG.SMSG_PONG);
				var w = p.GetWriter();
				w.Write(r.ReadUInt32());
				this.WritePacket(p);
				return;
			}
			_server.Handler.Handle(this, packet);
		}

		public override IPacket ReadPacket() {
			var data = this.GetStream();
			var head = new BinaryReader(_firstPacket ? data : new CryptoStream(data, _decryptor, CryptoStreamMode.Read));

			int size = IPAddress.NetworkToHostOrder(head.ReadInt16());
			int code = head.ReadInt32();

			using(var reader = new BinaryReader(data)) {
				return new WorldPacket((WMSG)code, reader.ReadBytes(size - 4));
			}
		}

		public override void WritePacket(IPacket packet) {
			var data = this.GetStream();
			var head = _firstPacket ? data : new CryptoStream(data, _encryptor, CryptoStreamMode.Write);
			packet.WriteHead(head);
			packet.WriteBody(data);
		}
	}
}
