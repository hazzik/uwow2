using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using Hazzik.Cryptography;
using Hazzik.Data;
using Hazzik.Data.NH;
using Hazzik.Helper;
using ICSharpCode.SharpZipLib.Zip.Compression.Streams;

namespace Hazzik.Net {
	public class WorldPacketProcessor : IPacketProcessor {
		private static readonly IRealmAccountDao _dao = new NHRealmAccountRepository();

		private static readonly HashAlgorithm _hmac =
			new HMACSHA1(new byte[]
			             { 0x38, 0xA7, 0x83, 0x15, 0xF8, 0x92, 0x25, 0x30, 0x71, 0x98, 0x67, 0xB1, 0x8C, 0x4, 0xE2, 0xAA });

		private readonly WorldClient _client;
		public readonly uint _seed = (uint)(new Random().Next(0, Int32.MaxValue));

		public WorldPacketProcessor(WorldClient client) {
			_client = client;
			_client.Send(GetAuthChallengePkt());
		}

		#region IPacketProcessor Members

		public void Process(IPacket packet) {
			int size = packet.Size;
			var code = (WMSG)packet.Code;
			Console.WriteLine("Handle {0}", code);
			if(code == WMSG.CMSG_AUTH_SESSION) {
				HandleAuthSession(packet);
			}
			else if(code == WMSG.CMSG_PING) {
				var r = packet.CreateReader();
				_client.Send(GetPongPkt(r.ReadUInt32()));
			}
			else {
				WorldServer.Handler.Handle(_client, packet);
			}
		}

		#endregion

		private IPacket GetAuthChallengePkt() {
			var result = new WorldPacket(WMSG.SMSG_AUTH_CHALLENGE);
			BinaryWriter w = result.CreateWriter();
			w.Write(_seed);
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
			byte[] clientDigest = r.ReadBytes(20);

			_client.Account = _dao.FindByName(accountName);

			_client.SetSymmetricAlgorithm(new SRP6Wow(_hmac.ComputeHash(_client.Account.SessionKey)));

			if(!Utility.Equals(clientDigest, ComputeServerDigest(clientSeed))) {
				throw new Exception();
			}

			_client.Send(GetAuthResponcePkt());

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
			_client.Send(GetTutorialFlagsPkt());
		}
 
		private IPacket GetAuthResponcePkt() {
			var result = new WorldPacket(WMSG.SMSG_AUTH_RESPONSE);
			BinaryWriter w = result.CreateWriter();
			w.Write((byte)0x0C);
			w.Write((uint)0);
			w.Write((byte)2);
			w.Write((uint)0);
			w.Write((byte)_client.Account.Expansion);
			return result;
		}

		private static IPacket GetAddonInfoPkt() {
			var result = new WorldPacket(WMSG.SMSG_ADDON_INFO);
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
				w.Write(Encoding.UTF8.GetBytes(_client.Account.Name));
				w.Write(0);
				w.Write(clientSeed);
				w.Write(_seed);
				w.Write(_client.Account.SessionKey);
				w.Flush();
				buff = ((MemoryStream)w.BaseStream).ToArray();
				buff = SHA1.Create().ComputeHash(buff, 0, buff.Length);
			}
			return buff;
		}

		public static IPacket GetPongPkt(uint ping) {
			var result = new WorldPacket(WMSG.SMSG_PONG);
			BinaryWriter w = result.CreateWriter();
			w.Write(ping);
			return result;
		}

		public static IPacket GetTutorialFlagsPkt() {
			var result = new WorldPacket(WMSG.SMSG_TUTORIAL_FLAGS);
			BinaryWriter w = result.CreateWriter();
			for(int i = 0; i < 32; i++) {
				w.Write((byte)0xff);
			}
			return result;
		}
	}
}