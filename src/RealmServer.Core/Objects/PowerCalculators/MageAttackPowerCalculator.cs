using System;

namespace Hazzik.Objects.PowerCalculators {
	public class MageAttackPowerCalculator : IAttackPowerCalculator {
		private readonly Unit _unit;

		public MageAttackPowerCalculator(Unit unit) {
			_unit = unit;
		}

		#region IAttackPowerCalculator Members

		public uint Base() {
			return _unit.Stat0 - 10;
		}

		#endregion
	}
}