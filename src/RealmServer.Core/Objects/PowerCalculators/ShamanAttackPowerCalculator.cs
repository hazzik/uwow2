using System;

namespace Hazzik.Objects.PowerCalculators {
	public class ShamanAttackPowerCalculator : IAttackPowerCalculator {
		private readonly Unit _unit;

		public ShamanAttackPowerCalculator(Unit unit) {
			_unit = unit;
		}

		#region IAttackPowerCalculator Members

		public uint Base() {
			return _unit.Stat0 + _unit.Stat1 + _unit.Level * 2 - 20;
		}

		#endregion
	}
}