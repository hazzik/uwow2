using System;
using Hazzik.Objects;

namespace Hazzik.Net {
	public interface ISession {
		Account Account { get; set; }
		Player Player { get; set; }
		IPacketSender Client { get; }
	}
}