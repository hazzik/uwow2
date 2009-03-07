using System;

namespace Hazzik.Net {
	public interface ISession {
		void ProcessData(IPacket packet);
		IPacket ReadPacket();
		void Send(IPacket packet);
	}
}