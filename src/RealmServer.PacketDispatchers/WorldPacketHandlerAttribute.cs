using System;

namespace Hazzik.RealmServer.PacketDispatchers {
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
	internal class WorldPacketHandlerAttribute : Attribute {
		private readonly WMSG messageType;

		public WorldPacketHandlerAttribute(WMSG messageType) {
			this.messageType = messageType;
		}

		public WMSG MessageType {
			get { return messageType; }
		}
	}
}