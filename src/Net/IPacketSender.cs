using System;

namespace Hazzik.Net {
	public interface IPacketSender {
		void Send(IPacket packet);
	}
}

