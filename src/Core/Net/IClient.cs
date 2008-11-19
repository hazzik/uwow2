using System;
using System.IO;

namespace Hazzik.Net {
	public interface IClient {
		Stream GetStream();
		void ProcessData(IPacket packet);
		IPacket ReadPacket();
		void SendPacket(IPacket packet);
	}
}