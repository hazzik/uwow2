using System;

namespace Hazzik.Data {
	public class Repository {
		public static readonly IAccountRepository Account = IoC.Resolve<IAccountRepository>();
		public static readonly ICreatureTemplateRepository CreatureTemplate = IoC.Resolve<ICreatureTemplateRepository>();
		public static readonly IGameObjectTemplateRepository GameObjectTemplate = IoC.Resolve<IGameObjectTemplateRepository>();
		public static readonly IItemTemplateRepository ItemTemplate = IoC.Resolve<IItemTemplateRepository>();
		public static readonly INpcTextRepository NpcText = IoC.Resolve<INpcTextRepository>();
		public static readonly IPlayerRepository Player = IoC.Resolve<IPlayerRepository>();
	}
}