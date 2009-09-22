using System;

namespace Hazzik {
	public interface IPacketDispatcherFactory {
		IPacketDispatcher GetDispatcher(WMSG wmsg);
	}
}