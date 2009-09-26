using System;
using System.IO;
using System.Text;
using Hazzik.Net;
using ICSharpCode.SharpZipLib.Zip.Compression.Streams;

namespace Hazzik.RealmServer.PacketDispatchers.Internal {
	[PacketHandlerClass(WMSG.CMSG_UPDATE_ACCOUNT_DATA)]
	internal class UpdateAccountDataDispatcher : IPacketDispatcher {
		#region IPacketDispatcher Members

		public void Dispatch(ISession session, IPacket packet) {
			BinaryReader reader = packet.CreateReader();
			var type = (AccountDataType)reader.ReadUInt32();
			DateTime time = DataTimeExtension.ToDateTime(reader.ReadUInt32());
			var decompressedSize = reader.ReadInt32();
			reader = new BinaryReader(new InflaterInputStream(reader.BaseStream));
			string data = Encoding.UTF8.GetString(reader.ReadBytes(decompressedSize));

			IPacket pkt = WorldPacketFactory.Create(WMSG.SMSG_UPDATE_ACCOUNT_DATA_COMPLETE);
			BinaryWriter writer = pkt.CreateWriter();
			writer.Write((uint)type);
			writer.Write(0);
			session.Client.Send(pkt);
		}

		#endregion
	}
}