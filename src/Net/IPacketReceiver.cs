using System;

namespace Hazzik.Net {
	public interface IPacketReceiver {
		IPacket Receive();
	}
}