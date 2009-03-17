using System;
using System.Collections.Generic;
using System.Linq;
using Hazzik.Net;

namespace Hazzik.Objects {
	partial class Player {
		public IPacket GetLoginVerifyWorldPkt() {
			var result = new WorldPacket(WMSG.SMSG_LOGIN_VERIFY_WORLD);
			var writer = result.CreateWriter();
			writer.Write(MapId);
			writer.Write(PosX);
			writer.Write(PosY);
			writer.Write(PosZ);
			writer.Write(Facing);
			return result;
		}

		private static IPacket GetUpdateObjectPkt(IEnumerable<ObjectUpdater> updaters) {
			var result = new WorldPacket(WMSG.SMSG_UPDATE_OBJECT);
			var writer = result.CreateWriter();
			writer.Write(updaters.Count());
			foreach(var updater in updaters) {
				updater.WriteUpdate(writer);
			}
			return result;
		}

		public IPacket GetNameQueryResponcePkt() {
			var result = new WorldPacket(WMSG.SMSG_NAME_QUERY_RESPONSE);
			var writer = result.CreateWriter();
			writer.Write(Guid);
			writer.WriteCString(Name);
			writer.WriteCString("");
			writer.Write((int)Race);
			writer.Write((int)Gender);
			writer.Write((int)Classe);
			writer.Write((byte)01);
			writer.WriteCString(Name);
			writer.WriteCString(Name);
			writer.WriteCString(Name);
			writer.WriteCString(Name);
			writer.WriteCString(Name);
			return result;
		}
	}
}