using System;
using Hazzik.GameObjects;

namespace Hazzik.Data {
	public interface IGameObjectTemplateRepository : IDao<GameObjectTemplate>, IFindById<GameObjectTemplate> {
	}
}