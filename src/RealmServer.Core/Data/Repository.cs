using System;
using Hazzik.Data.NH;

namespace Hazzik.Data {
	public class Repository {
		public static readonly IAccountRepository Account = new NHAccountRepository();
		public static readonly IPlayerRepository Player = new NHPlayerRepository();
		public static readonly IGameObjectTemplateRepository GameObjectTemplate = new NHGameObjectTemplateRepository();
	}
}