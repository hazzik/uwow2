using System;

namespace Hazzik.Objects.Update {
	internal class UpdateTimer : Timer2 {
		private readonly UpdateManager _manager;

		public UpdateTimer(UpdateManager manager)
			: base(1000) {
			_manager = manager;
		}

		public override void OnTick() {
			_manager.UpdateObjects();
		}
	}
}