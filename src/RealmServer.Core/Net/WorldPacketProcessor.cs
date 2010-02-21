using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using Hazzik.Cryptography;
using Hazzik.Data;
using Hazzik.IO;
using ICSharpCode.SharpZipLib.Zip.Compression.Streams;

namespace Hazzik.Net {
	public class WorldPacketProcessor : IPacketProcessor {
		private readonly uint serverSeed = (uint)(new Random().Next(0, Int32.MaxValue));
		private readonly ISession session;
		private readonly ICryptor cryptor;

		public WorldPacketProcessor(ISession client, ICryptor cryptor) {
			this.cryptor = cryptor;
			session = client;
			session.Send(GetAuthChallengePkt());
		}

		public static IPacketDispatcherFactory Factory { get; set; }

		#region IPacketProcessor Members

		public void Process(IPacket packet) {
			var code = (WMSG)packet.Code;
			if(code == WMSG.CMSG_AUTH_SESSION) {
				HandleAuthSession(packet);
				return;
			}
			IPacketDispatcher dispatcher = Factory.GetDispatcher(code);
			if(dispatcher != null) {
				Console.WriteLine(code);
				dispatcher.Dispatch(session, packet);
			}
			else {
				ConsoleColor color = Console.ForegroundColor;
				Console.ForegroundColor = ConsoleColor.Red;
				Console.WriteLine(code);
				Console.ForegroundColor = color;
			}
		}

		#endregion

		private IPacket GetAuthChallengePkt() {
			IPacket result = WorldPacketFactory.Create(WMSG.SMSG_AUTH_CHALLENGE);
			BinaryWriter w = result.CreateWriter();
			w.Write(1);
			w.Write(serverSeed);
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

			session.Account = IoC.Resolve<IAccountRepository>().FindByName(accountName);

			cryptor.SetSymmetricAlgorithm(new WowCryptRC4(session.Account.SessionKey));

			if(!ByteArrayExtensions.Equals(clientDigest, ComputeServerDigest(clientSeed))) {
				throw new Exception();
			}

			session.Send(GetAuthResponcePkt());

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
			session.Send(GetTutorialFlagsPkt());
			session.SendAccountDataTimes(0x15);
		}

		private IPacket GetAuthResponcePkt() {
			IPacket result = WorldPacketFactory.Create(WMSG.SMSG_AUTH_RESPONSE);
			BinaryWriter w = result.CreateWriter();
			w.Write((byte)0x0C);
			w.Write((uint)0);
			w.Write((byte)2);
			w.Write((uint)0);
			w.Write((byte)session.Account.Expansion);
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
				w.Write(Encoding.UTF8.GetBytes(session.Account.Name));
				w.Write(0);
				w.Write(clientSeed);
				w.Write(serverSeed);
				w.Write(session.Account.SessionKey);
				w.Flush();
				buff = ((MemoryStream)w.BaseStream).ToArray();
				buff = SHA1.Create().ComputeHash(buff, 0, buff.Length);
			}
			return buff;
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