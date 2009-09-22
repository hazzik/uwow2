using System;

namespace Hazzik.Net {
	public interface IPacketDispatcher {
		void Dispatch(ISession session, IPacket packet);
	}
}