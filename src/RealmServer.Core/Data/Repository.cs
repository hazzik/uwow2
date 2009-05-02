using System;
using StructureMap;

namespace Hazzik.Data {
	public class Repository {
		public static readonly IAccountRepository Account = ObjectFactory.GetInstance<IAccountRepository>();
		public static readonly ICreatureTemplateRepository CreatureTemplate = ObjectFactory.GetInstance<ICreatureTemplateRepository>();
		public static readonly IGameObjectTemplateRepository GameObjectTemplate = ObjectFactory.GetInstance<IGameObjectTemplateRepository>();
		public static readonly IItemTemplateRepository ItemTemplate = ObjectFactory.GetInstance<IItemTemplateRepository>();
		public static readonly INpcTextRepository NpcText = ObjectFactory.GetInstance<INpcTextRepository>();
		public static readonly IPlayerRepository Player = ObjectFactory.GetInstance<IPlayerRepository>();
	}
}