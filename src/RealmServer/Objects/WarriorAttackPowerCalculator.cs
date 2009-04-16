using System;

namespace Hazzik.Objects {
	public class WarriorAttackPowerCalculator : IAttackPowerCalculator {
		private readonly Unit _unit;

		public WarriorAttackPowerCalculator(Unit unit) {
			_unit = unit;
		}

		#region IAttackPowerCalculator Members

		public uint Base() {
			return _unit.Stat0 * 2 + _unit.Level * 3 - 20;
		}

		#endregion
	}
}