using System;
using System.IO;
using Hazzik.Net;

namespace Hazzik.RealmServer.PacketDispatchers.Internal {
	[PacketHandlerClass(WMSG.CMSG_CAST_SPELL)]
	internal class CastSpellDispatcher : IPacketDispatcher {
		#region IPacketDispatcher Members

		public void Dispatch(ISession session, IPacket packet) {
			BinaryReader reader = packet.CreateReader();
			byte castCount = reader.ReadByte();
			uint spellId = reader.ReadUInt32();
			byte unklags = reader.ReadByte();

			IPacket pkt = WorldPacketFactory.Create(WMSG.SMSG_SPELL_GO);
			BinaryWriter writer = pkt.CreateWriter();
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

			packet = WorldPacketFactory.Create(WMSG.SMSG_AURA_UPDATE);
			writer = packet.CreateWriter();
			writer.WritePackGuid(session.Player.Guid);
			writer.Write((byte)0);
			writer.Write(spellId);
			writer.Write((byte)0);
			writer.Write((byte)1);
			writer.Write((byte)0);
			session.Client.Send(packet);
		}

		#endregion
	}
}