using System;
using Hazzik.Net;

namespace Hazzik.Objects {
	partial class Unit {
		private IPacket GetPowerUpdatePkt(uint value) {
			var packet = new WorldPacket(WMSG.SMSG_POWER_UPDATE);
			var writer = packet.CreateWriter();
			writer.WritePackGuid(Guid);
			writer.Write((byte)PowerType);
			writer.Write(value);
			return packet;
		}
	}
}