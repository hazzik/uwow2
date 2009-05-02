using System;
using StructureMap;

namespace Hazzik.Data {
	public class Repository {
		public static readonly IAccountRepository Account = ObjectFactory.GetInstance<IAccountRepository>();
		public static readonly IPlayerRepository Player = ObjectFactory.GetInstance<IPlayerRepository>();
		public static readonly IGameObjectTemplateRepository GameObjectTemplate = ObjectFactory.GetInstance<IGameObjectTemplateRepository>();
	}
}