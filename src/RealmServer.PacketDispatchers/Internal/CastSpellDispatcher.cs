using System;
using Hazzik.Net;

namespace Hazzik.RealmServer.PacketDispatchers.Internal {
	[PacketHandlerClass(WMSG.CMSG_CAST_SPELL)]
	internal class CastSpellDispatcher : IPacketDispatcher {
		#region IPacketDispatcher Members

		public void Dispatch(ISession session, IPacket packet) {
			var reader = packet.CreateReader();
			var castCount = reader.ReadByte();
			var spellId = reader.ReadUInt32();
			var unklags = reader.ReadByte();

			var pkt = WorldPacketFactory.Create(WMSG.SMSG_SPELL_GO);
			var writer = pkt.CreateWriter();
			writer.WritePackGuid(session.Player.Guid);
			writer.WritePackGuid(session.Player.Guid);
			writer.Write(castCount);
			writer.Write(spellId);
			writer.Write(0);
			writer.Write(0);
			writer.Write((ushort)0);
			writer.Write((byte)1);
			writer.Write(session.Player.Guid);
			writer.Write((byte)0);
			session.Client.Send(pkt);
		}

		#endregion
	}
}