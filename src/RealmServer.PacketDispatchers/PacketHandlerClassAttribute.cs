using System;

namespace Hazzik.RealmServer.PacketDispatchers {
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
	internal class PacketHandlerClassAttribute : Attribute {
		private readonly WMSG _messageType;

		public PacketHandlerClassAttribute(WMSG messageType) {
			_messageType = messageType;
		}

		public WMSG MessageType {
			get { return _messageType; }
		}
	}
}