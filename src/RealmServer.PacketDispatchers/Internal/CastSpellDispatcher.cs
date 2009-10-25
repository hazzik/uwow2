using System;
using System.IO;
using System.Threading;
using Hazzik.IO;
using Hazzik.Net;


namespace Hazzik.RealmServer.PacketDispatchers.Internal {
	[WorldPacketHandler(WMSG.CMSG_CAST_SPELL)]
	internal class CastSpellDispatcher : IPacketDispatcher {
		#region IPacketDispatcher Members

		public void Dispatch(ISession session, IPacket packet) {
			BinaryReader reader = packet.CreateReader();
			byte castCount = reader.ReadByte();
			uint spellId = reader.ReadUInt32();
			byte unklags = reader.ReadByte();


			IPacket pkt = WorldPacketFactory.Create(WMSG.SMSG_SPELL_START);
			BinaryWriter writer = pkt.CreateWriter();
			writer.WritePackGuid(session.Player.Guid);
			writer.WritePackGuid(session.Player.Guid);
			writer.Write(castCount);
			writer.Write(spellId);
			writer.Write(0); //cast flags
			writer.Write(0); //ticks count
			writer.Write(0); //targetflags
			session.Client.Send(pkt);

			Thread.Sleep(5000);
			pkt = WorldPacketFactory.Create(WMSG.SMSG_SPELL_GO);
			writer = pkt.CreateWriter();
			writer.WritePackGuid(session.Player.Guid);
			writer.WritePackGuid(session.Player.Guid);
			writer.Write(castCount);
			writer.Write(spellId);
			writer.Write(0); //cast flags
			writer.Write(0); //ticks count
			writer.Write((byte)1); //hit count
			writer.Write(session.Player.Guid);
			writer.Write((byte)0); //miss count
			writer.Write(0); // targetflags
			session.Client.Send(pkt);
		}

		#endregion
	}
}