using System;

namespace Hazzik.Net {
	public interface IAsyncPacketReceiver {
		void ReceiveAsync(Action<IPacket> callback);
	}
}