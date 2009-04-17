using System;
using Hazzik.Objects;

namespace Hazzik.Net {
	public interface ISession : IPacketSender {
		Account Account { get; set; }
		Player Player { get; set; }
		IPacketSender Client { get; }
	}
}