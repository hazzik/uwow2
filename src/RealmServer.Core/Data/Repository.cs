using System;

namespace Hazzik.Data {
	public class Repository {
		public static readonly IAccountRepository Account = ServiceLocator.Resolve<IAccountRepository>();
		public static readonly ICreatureTemplateRepository CreatureTemplate = ServiceLocator.Resolve<ICreatureTemplateRepository>();
		public static readonly IGameObjectTemplateRepository GameObjectTemplate = ServiceLocator.Resolve<IGameObjectTemplateRepository>();
		public static readonly IItemTemplateRepository ItemTemplate = ServiceLocator.Resolve<IItemTemplateRepository>();
		public static readonly INpcTextRepository NpcText = ServiceLocator.Resolve<INpcTextRepository>();
		public static readonly IPlayerRepository Player = ServiceLocator.Resolve<IPlayerRepository>();
	}
}