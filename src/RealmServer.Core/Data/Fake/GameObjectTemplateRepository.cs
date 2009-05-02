using System.Collections.Generic;
using Hazzik.GameObjects;

namespace Hazzik.Data.Fake {
	public class GameObjectTemplateRepository : IGameObjectTemplateRepository {
		private readonly IDictionary<uint, GameObjectTemplate> _templates = GetTemplates();

		public static Dictionary<uint, GameObjectTemplate> GetTemplates() {
			var chair = new GameObjectTemplate { Id = 2489, Name = "Chair", Type = GameObjectType.Chair, DisplayId = 91, ScaleX = 1f, };
			chair.Fields[0] = 1;
			chair.Fields[1] = 1;
			var templates = new Dictionary<uint, GameObjectTemplate> {
				{ 176497, new GameObjectTemplate { Id = 176497, Name = "Portal to Ironforge", Type = GameObjectType.SpellCaster, DisplayId = 4394, ScaleX = 1f, } },
				{ chair.Id, chair }
			};
			return templates;
		}

		public GameObjectTemplate FindById(uint id) {
			GameObjectTemplate template;
			_templates.TryGetValue(id, out template);
			return template;
		}
	}
}