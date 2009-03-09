using System;

namespace Hazzik.Net {
	public interface ISession  {
		void Send(IPacket packet);
	}
}