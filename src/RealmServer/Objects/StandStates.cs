using System;

namespace Hazzik {
	public enum SheathType : sbyte {
		None = 0,
		Melee = 1,
		Ranged = 2,
		Shield = 4,
		Rod = 5,
		Light = 7,
	}

	public enum StandStates {
		Standing = 0,
		Sitting = 1,
		SittingChair = 2,
		Sleeping = 3,
		SittingChairLow = 4,
		SittingChairMedium = 5,
		SittingChairHigh = 6,
		Dead = 7,
		Kneel = 8,
	}
	public enum BattleStances {
		CatForm = 0x1,
		TreeFrom = 0x2,
		TravelForm = 0x3,
		AquaticForm = 0x4,
		BearForm = 0x5,
		Ambient = 0x6,
		Goul = 0x7,
		DireBearForm = 0x8,
		GhostWolf = 0x10,
		BattleStance = 0x11,
		DefensiveStance = 0x12,
		BerserkerStance = 0x13,
		ShadowForm = 0x1C,
		Stealth = 0x1E,
		SwiftFlightForm = 0x1B,
		FlightForm = 0x1D,
		MoonkinForm = 0x1F,
		SpiritOfRedemption = 0x20,
	}
}