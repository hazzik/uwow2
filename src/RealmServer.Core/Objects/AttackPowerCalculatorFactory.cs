using System;
using Hazzik.Objects.PowerCalculators;

namespace Hazzik.Objects {
	public class AttackPowerCalculatorFactory {
		public static IAttackPowerCalculator CreateAttackPowerCalculator(Unit unit) {
			switch(unit.Classe) {
			case Classes.Warrior:
			case Classes.Paladin:
				return new WarriorAttackPowerCalculator(unit);
			case Classes.Hunter:
			case Classes.Rogue:
			case Classes.Shaman:
				return new ShamanAttackPowerCalculator(unit);
			case Classes.Priest:
			case Classes.Mage:
			case Classes.Warlock:
				return new MageAttackPowerCalculator(unit);
			case Classes.Druid:
				break;
			default:
				//throw new ArgumentOutOfRangeException();
				break;
			}
			return new WarriorAttackPowerCalculator(unit);
		}
	}
}