using System;

namespace Hazzik.Net {
	public interface IAsyncPacketReader {
		void ReadPacketAsync(Action<IPacket> callback);
	}
}