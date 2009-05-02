using Hazzik.GameObjects;

namespace Hazzik.Data {
	public interface IGameObjectTemplateRepository {
		GameObjectTemplate FindById(uint id);
	}
}