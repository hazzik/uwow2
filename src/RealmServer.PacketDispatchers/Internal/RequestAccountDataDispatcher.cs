using System;
using System.IO;
using System.Linq;
using Hazzik.Net;
using ICSharpCode.SharpZipLib.Zip.Compression.Streams;

namespace Hazzik.RealmServer.PacketDispatchers.Internal {
	[WorldPacketHandler(WMSG.CMSG_REQUEST_ACCOUNT_DATA)]
	internal class RequestAccountDataDispatcher : IPacketDispatcher {
		#region IPacketDispatcher Members

		public void Dispatch(ISession session, IPacket packet) {
			BinaryReader reader = packet.CreateReader();
			var type = (AccountDataType)reader.ReadUInt32();
			ulong guid = session.Player != null ? session.Player.Guid : 0;

			AccpuntData accpuntData = session.Account.FindAccpuntData(type, guid);

			session.Client.Send(GetUpdateAccountDataPkt(accpuntData));
		}

		private static IPacket GetUpdateAccountDataPkt(AccpuntData accpuntData) {
			IPacket packet = WorldPacketFactory.Create(WMSG.SMSG_UPDATE_ACCOUNT_DATA);
			BinaryWriter writer = packet.CreateWriter();
			writer.Write(accpuntData.Guid);
			writer.Write((uint)accpuntData.Type);
			writer.Write(accpuntData.Time.ToUnixTimestamp());
			writer = new BinaryWriter(new DeflaterOutputStream(writer.BaseStream));
			writer.WriteCString(accpuntData.Data);
			writer.Flush();
			return packet;
		}

		#endregion
	}
}