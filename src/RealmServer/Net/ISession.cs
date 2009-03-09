using System;
using Hazzik.Objects;

namespace Hazzik.Net {
	public interface ISession  {
		void Send(IPacket packet);
		Account Account { get; set; }
		Player Player { get; set; }
	}
}