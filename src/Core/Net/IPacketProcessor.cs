using System;

namespace Hazzik.Net {
	public interface IPacketProcessor {
		void Process(IPacket packet);
	}
}