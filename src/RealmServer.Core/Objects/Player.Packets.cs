using System;
using Hazzik.Net;

namespace Hazzik.Objects {
	partial class Player {
		public IPacket GetLoginVerifyWorldPkt() {
			var result = WorldPacketFactory.Create(WMSG.SMSG_LOGIN_VERIFY_WORLD);
			var writer = result.CreateWriter();
			writer.Write(MapId);
			writer.Write(PosX);
			writer.Write(PosY);
			writer.Write(PosZ);
			writer.Write(Facing);
			return result;
		}

		public IPacket GetNameQueryResponcePkt() {
			var result = WorldPacketFactory.Create(WMSG.SMSG_NAME_QUERY_RESPONSE);
			var writer = result.CreateWriter();
			writer.WritePackGuid(Guid);
			writer.Write((byte)0);// this is a type, ranging from 0-3
			writer.WriteCString(Name);
			writer.WriteCString("");
			writer.Write((byte)Race);
			writer.Write((byte)Gender);
			writer.Write((byte)Classe);
			writer.Write(true);
			writer.WriteCString(Name);
			writer.WriteCString(Name);
			writer.WriteCString(Name);
			writer.WriteCString(Name);
			return result;
		}
	}
}