using System;

namespace Hazzik.Net {
	public interface IPacketDispatcherFactory {
		IPacketDispatcher GetDispatcher(WMSG wmsg);
	}
}