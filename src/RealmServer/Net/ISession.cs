using System;
using Hazzik.Objects;

namespace Hazzik.Net {
	public interface ISession : IClient {
		Account Account { get; set; }
		Player Player { get; set; }
	}
}