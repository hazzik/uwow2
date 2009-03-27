using System;
using Hazzik.Data;
using Hazzik.Data.NH;

namespace Hazzik.Net {
	public class Repositories {
		public static readonly IAccountRepository Account = new NHAccountRepository();
		public static readonly IPlayerRepository Player = new NHPlayerRepository();
	}
}