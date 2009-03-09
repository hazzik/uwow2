using System;

namespace Hazzik.Net {
	public interface ISession  {
		IPacket ReadPacket();
		void Send(IPacket packet);
	}
}