using System;

namespace Hazzik.Net {
	public interface IClient {
		void Send(IPacket packet);
	}
}

