using System;
namespace Hazzik.Net {
	public interface IClient {
		System.IO.Stream GetStream();
		void ProcessData(IPacket packet);
		IPacket ReadPacket();
		void SendPacket(IPacket packet);
	}
}
