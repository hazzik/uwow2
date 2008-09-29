using System;
namespace Hazzik.Net {
	interface IClient {
		System.IO.Stream GetStream();
		void ProcessData(IPacket packet);
		IPacket ReadPacket();
		void SendPacket(IPacket packet);
	}
}
