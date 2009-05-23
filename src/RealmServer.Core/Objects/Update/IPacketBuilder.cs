using System;
using Hazzik.Net;

namespace Hazzik.Objects.Update {
	public interface IPacketBuilder {
		bool IsEmpty { get; }
		IPacket Build();
	}
}