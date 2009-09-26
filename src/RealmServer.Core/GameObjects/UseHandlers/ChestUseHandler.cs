using System;
using Hazzik.Objects;

namespace Hazzik.GameObjects.UseHandlers {
	internal class ChestUseHandler : IGameObjectUseHandler {
		private readonly GameObject gameObject;

		public ChestUseHandler(GameObject gameObject) {
			this.gameObject = gameObject;
		}

		#region IGameObjectUseHandler Members

		public bool Use(Player user) {
			gameObject.Flags = 0;

			return true;
		}

		#endregion
	}
}