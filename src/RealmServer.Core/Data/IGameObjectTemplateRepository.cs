using System;
using Hazzik.GameObjects;

namespace Hazzik.Data {
	public interface IGameObjectTemplateRepository : IDao<GameObjectTemplate> {
		GameObjectTemplate FindById(uint id);
	}
}