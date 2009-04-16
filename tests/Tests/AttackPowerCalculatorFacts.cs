using System;
using Hazzik.Objects;
using Xunit;

namespace Tests {
	/// <summary>
	/// Class ;  Melee Base ;  Ranged Base
	/// Death Knight; (Strength x 2) - 20 ; 
	/// Druid ; (Strength x 2) - 20 ; Agility - 10
	/// Druid (Bear); (Strength x 2) + (Character Level x 3) - 20 ; 
	/// Druid (Cat) ; (Strength x 2) + Agility + (Character Level x 2) - 20 ; 
	/// Druid (Moonkin); (Strength x 2) + (Character Level x 1.5) - 20 ; 
	/// Hunter ; Strength + Agility + (Character Level x 2) - 20 ; (Character Level x 2) + Agility - 10
	/// Mage ; Strength - 10 ; Agility - 10
	/// Paladin; (Strength x 2) + (Character Level x 3) - 20 ; Agility - 10
	/// Priest; Strength - 10 ; Agility - 10
	/// Rogue; Strength + Agility + (Character Level x 2) - 20 ; Character Level + Agility - 10
	/// Shaman; Strength + Agility + (Character Level x 2) - 20 ; Agility - 10
	/// Warlock; Strength - 10 ; Agility - 10
	/// Warrior; (Strength x 2) + (Character Level x 3) - 20 ; Character Level + Agility - 10 
	/// </summary>
	public class AttackPowerCalculatorFacts {
		private static Unit CreatePlayerWithRandomLevelStrength() {
			return new Player {
				Level = (uint)(new Random().Next() % 80),
				Stat0 = (uint)(new Random().Next() % 100),
			};
		}

		private static Unit CreatePlayerWithRandomLevelStrengthAgility() {
			return new Player {
				Level = (uint)(new Random().Next() % 80),
				Stat0 = (uint)(new Random().Next() % 100),
				Stat1 = (uint)(new Random().Next() % 100),
			};
		}

		///Warrior		AP:(Strength x 2) + (Level x 3) - 20 RAP:Level + Agility - 10 
		///Druid (Bear)AP:(Strength x 2) + (Level x 3) - 20 RAP:0	
		///Paladin		AP:(Strength x 2) + (Level x 3) - 20 RAP:Agility - 10
		[Fact]
		public void WarriorAttackPowerCalculator() {
			var unit = CreatePlayerWithRandomLevelStrength();
			var calculator = new WarriorAttackPowerCalculator(unit);
			Assert.Equal(unit.Stat0 * 2 + unit.Level * 3 - 20, calculator.Base());
		}

		///Mage		AP:Strength - 10 RAP:Agility - 10
		///Priest	AP:Strength - 10 RAP:Agility - 10
		///Warlock	AP:Strength - 10 RAP:Agility - 10
		[Fact]
		public void MageAttackPowerCalculator() {
			var unit = CreatePlayerWithRandomLevelStrength();
			var calcualator = new MageAttackPowerCalculator(unit);
			Assert.Equal(unit.Stat0 - 10, calcualator.Base());
		}

		///Shaman	AP:Strength + Agility + (Level x 2) - 20 RAP:Agility - 10
		///Rogue		AP:Strength + Agility + (Level x 2) - 20 RAP:Character Level + Agility - 10
		///Hunter 	AP:Strength + Agility + (Level x 2) - 20 RAP:(Character Level x 2) + Agility - 10	
		[Fact]
		public void ShamanAttackPowerCalculator() {
			Unit unit = CreatePlayerWithRandomLevelStrengthAgility();
			var calcualator = new ShamanAttackPowerCalculator(unit);
			Assert.Equal(unit.Stat0 + unit.Stat1 + unit.Level * 2 - 20, calcualator.Base());
		}
	}
}