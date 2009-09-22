using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using Hazzik.Cryptography;
using Hazzik.Data;
using Hazzik.Helper;
using ICSharpCode.SharpZipLib.Zip.Compression.Streams;

namespace Hazzik.Net {
	public class WorldPacketProcessor : IPacketProcessor {
		private readonly uint _seed = (uint)(new Random().Next(0, Int32.MaxValue));
		private readonly ISession _session;

		public WorldPacketProcessor(ISession client) {
			_session = client;
			_session.Client.Send(GetAuthChallengePkt());
		}

		public static IPacketDispatcherFactory Factory { get; set; }

		#region IPacketProcessor Members

		public void Process(IPacket packet) {
			int size = packet.Size;
			var code = (WMSG)packet.Code;
			//Console.WriteLine("Handle {0}", code);
			if(code == WMSG.CMSG_AUTH_SESSION) {
				HandleAuthSession(packet);
				return;
			}
			if(code == WMSG.CMSG_PING) {
				(_session.Client).Send(GetPongPkt(packet.CreateReader().ReadUInt32()));
				return;
			}
			IPacketDispatcher dispatcher = Factory.GetDispatcher((WMSG)packet.Code);
			if(dispatcher != null) {
				Console.WriteLine((WMSG)packet.Code);
				dispatcher.Dispatch(_session, packet);
			}
			else {
				ConsoleColor color = Console.ForegroundColor;
				Console.ForegroundColor = ConsoleColor.Red;
				Console.WriteLine((WMSG)packet.Code);
				Console.ForegroundColor = color;
			}
		}

		#endregion

		private IPacket GetAuthChallengePkt() {
			IPacket result = WorldPacketFactory.Create(WMSG.SMSG_AUTH_CHALLENGE);
			BinaryWriter w = result.CreateWriter();
			w.Write(1);
			w.Write(_seed);
			w.Write(0);
			w.Write(0);
			w.Write(0);
			w.Write(0);
			return result;
		}

		private void HandleAuthSession(IPacket packet) {
			Stream dataStream = packet.GetStream();
			BinaryReader r = packet.CreateReader();
			uint version = r.ReadUInt32();
			uint unk2 = r.ReadUInt32();
			string accountName = r.ReadCString();
			uint unk = r.ReadUInt32();
			uint clientSeed = r.ReadUInt32();
			ulong unk3 = r.ReadUInt64();
			byte[] clientDigest = r.ReadBytes(20);

			_session.Account = Repository.Account.FindByName(accountName);

			_session.Client.SetSymmetricAlgorithm(new WowCryptRC4(_session.Account.SessionKey));

			if(!Utility.Equals(clientDigest, ComputeServerDigest(clientSeed))) {
				throw new Exception();
			}

			(_session.Client).Send(GetAuthResponcePkt());

			uint addonInfoBlockSize = r.ReadUInt32();
			dataStream = new InflaterInputStream(dataStream); //дальше данные запакованы
			r = new BinaryReader(dataStream);
			try {
				while(true) {
					var addonInfo = new AddonInfo {
						Name = r.ReadCString(),
						Crc = r.ReadUInt64(),
						Status = r.ReadByte(),
					};
					AddonManager.Instance[addonInfo.Name] = addonInfo;
				}
			}
			catch(Exception e) {
			}
			//_client.Send(GetAddonInfoPkt());
			(_session.Client).Send(GetTutorialFlagsPkt());
		}

		private IPacket GetAuthResponcePkt() {
			IPacket result = WorldPacketFactory.Create(WMSG.SMSG_AUTH_RESPONSE);
			BinaryWriter w = result.CreateWriter();
			w.Write((byte)0x0C);
			w.Write((uint)0);
			w.Write((byte)2);
			w.Write((uint)0);
			w.Write((byte)_session.Account.Expansion);
			return result;
		}

		private static IPacket GetAddonInfoPkt() {
			IPacket result = WorldPacketFactory.Create(WMSG.SMSG_ADDON_INFO);
			BinaryWriter w = result.CreateWriter();
			foreach(AddonInfo item in AddonManager.Instance.AddonInfos) {
				w.Write((ulong)0x0102);
			}
			w.Write(0);
			w.Flush();
			return result;
		}

		private byte[] ComputeServerDigest(uint clientSeed) {
			byte[] buff;
			using(var w = new BinaryWriter(new MemoryStream())) {
				w.Write(Encoding.UTF8.GetBytes(_session.Account.Name));
				w.Write(0);
				w.Write(clientSeed);
				w.Write(_seed);
				w.Write(_session.Account.SessionKey);
				w.Flush();
				buff = ((MemoryStream)w.BaseStream).ToArray();
				buff = SHA1.Create().ComputeHash(buff, 0, buff.Length);
			}
			return buff;
		}

		public static IPacket GetPongPkt(uint ping) {
			IPacket result = WorldPacketFactory.Create(WMSG.SMSG_PONG);
			BinaryWriter w = result.CreateWriter();
			w.Write(ping);
			return result;
		}

		public static IPacket GetTutorialFlagsPkt() {
			IPacket result = WorldPacketFactory.Create(WMSG.SMSG_TUTORIAL_FLAGS);
			BinaryWriter w = result.CreateWriter();
			for(int i = 0; i < 32; i++) {
				w.Write((byte)0xff);
			}
			return result;
		}
	}
}