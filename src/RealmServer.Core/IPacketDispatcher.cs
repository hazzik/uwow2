using System;
using Hazzik.Net;

namespace Hazzik {
	public interface IPacketDispatcher {
		void Dispatch(ISession session, IPacket packet);
	}
}