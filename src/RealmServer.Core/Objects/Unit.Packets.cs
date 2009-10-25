using System;
using System.IO;
using Hazzik.IO;
using Hazzik.Net;

namespace Hazzik.Objects {
	partial class Unit {
		private IPacket GetPowerUpdatePkt(uint value) {
			IPacket packet = WorldPacketFactory.Create(WMSG.SMSG_POWER_UPDATE);
			BinaryWriter writer = packet.CreateWriter();
			writer.WritePackGuid(Guid);
			writer.Write((byte)PowerType);
			writer.Write(value);
			return packet;
		}
	}
}