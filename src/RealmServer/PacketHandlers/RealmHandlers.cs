using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hazzik.Net;
using Hazzik.Helper;
using Hazzik.Attributes;

namespace Hazzik {
	[PacketHandlerClass]
	public static class RealmHandlers {
		[WorldPacketHandler(WMSG.CMSG_REALM_SPLIT)]
		public static void HandleCMSG_REALM_SPLIT(ClientBase client, IPacket packet) {
			var r = packet.GetReader();
			var unk1 = r.ReadUInt32();

			var responce = new WorldPacket(WMSG.SMSG_REALM_SPLIT);
			var w = responce.GetWriter();
			w.Write(unk1);
			//0-normal, 1-split, 2-split pending;
			w.Write(0);
			w.WriteCString(DateTime.Now.AddDays(1).ToShortDateString());
			w.WriteCString("hello");
			w.WriteCString("world");
			client.WritePacket(responce);
		}
	}
}
