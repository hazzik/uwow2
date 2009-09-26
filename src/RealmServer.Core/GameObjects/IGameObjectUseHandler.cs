using System;
using Hazzik.Objects;

namespace Hazzik.GameObjects {
	public interface IGameObjectUseHandler {
		bool Use(Player user);
	}
}