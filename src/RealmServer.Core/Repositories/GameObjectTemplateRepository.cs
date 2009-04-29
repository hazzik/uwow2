using System;
using System.Collections.Generic;
using Hazzik.GameObjects;

namespace Hazzik.Repositories {
	public class GameObjectTemplateRepository {
		private static readonly IDictionary<uint, GameObjectTemplate> Templates = GetTemplates();

		private static Dictionary<uint, GameObjectTemplate> GetTemplates() {
			var templates = new Dictionary<uint, GameObjectTemplate> {
				{ 176497, new GameObjectTemplate { Id = 176497, Name = "Portal to Ironforge", Type = GameObjectType.SpellCaster, DisplayId = 4394, ScaleX = 1f, } }
			};
			return templates;
		}

		public static GameObjectTemplate FindById(uint id) {
			GameObjectTemplate template;
			Templates.TryGetValue(id, out template);
			return template;
		}
	}
}