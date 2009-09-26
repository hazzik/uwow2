using System;
using System.IO;
using Hazzik.Net;
using ICSharpCode.SharpZipLib.Zip.Compression.Streams;

namespace Hazzik.RealmServer.PacketDispatchers.Internal {
	[PacketHandlerClass(WMSG.CMSG_REQUEST_ACCOUNT_DATA)]
	internal class RequestAccountDataDispatcher : IPacketDispatcher {
		#region IPacketDispatcher Members

		public void Dispatch(ISession session, IPacket packet) {
			BinaryReader reader = packet.CreateReader();
			var type = (AccountDataType)reader.ReadUInt32();

			IPacket pkt = WorldPacketFactory.Create(WMSG.SMSG_UPDATE_ACCOUNT_DATA);
			BinaryWriter writer = pkt.CreateWriter();
			writer.Write(session.Player != null ? session.Player.Guid : 0);
			writer.Write((uint)type);
			writer.Write(DateTime.Today.ToUnixTimestamp());
			writer = new BinaryWriter(new DeflaterOutputStream(writer.BaseStream));
			writer.WriteCString("");
			writer.Flush();
			session.Client.Send(pkt);
		}

		#endregion
	}
}