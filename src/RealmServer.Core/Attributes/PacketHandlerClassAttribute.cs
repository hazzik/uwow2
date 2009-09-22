using System;

namespace Hazzik.Attributes {
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
	public class PacketHandlerClassAttribute : Attribute {
		private readonly WMSG _messageType;

		public PacketHandlerClassAttribute(WMSG messageType) {
			_messageType = messageType;
		}

		public WMSG MessageType {
			get { return _messageType; }
		}
	}
}