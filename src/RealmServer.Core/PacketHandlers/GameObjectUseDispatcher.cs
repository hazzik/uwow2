using System;
using System.IO;
using System.Linq;
using Hazzik.Attributes;
using Hazzik.GameObjects;
using Hazzik.Map;
using Hazzik.Net;
using Hazzik.Objects;

namespace Hazzik.PacketHandlers {
	[PacketHandlerClass(WMSG.CMSG_GAMEOBJ_USE)]
	public class GameObjectUseDispatcher : IPacketDispatcher {
		#region IPacketDispatcher Members

		public void Dispatch(ISession client, IPacket packet) {
			BinaryReader reader = packet.CreateReader();
			ulong guid = reader.ReadUInt64();
			var go = ObjectManager.GetSeenObjectsNear(client.Player).Where(x => x.Guid == guid).FirstOrDefault() as GameObject;
			new ChairHandler(go).Use(client.Player);
		}

		#endregion
	}
}