using System;

namespace Hazzik.Objects.Update {
	internal class UpdateTimer : Timer2 {
		private readonly UpdateManager updateManager;

		public UpdateTimer(UpdateManager manager)
			: base(1000) {
			updateManager = manager;
		}

		public override void OnTick() {
			updateManager.UpdateObjects();
		}
	}
}