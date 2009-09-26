using System;
using Hazzik.Objects;

namespace Hazzik.GameObjects.UseHandlers {
	internal class NullUseHandler : IGameObjectUseHandler {
		#region IGameObjectUseHandler Members

		public bool Use(Player user) {
			return true;
		}

		#endregion
	}
}