using System;

namespace Hazzik.Creatures {
	[Flags]
	public enum NpcFlags {
		None = 0,
		Gossip = 0x1,
		QuestGiver = 0x2,
		//Flag_0X4 = 0x4,
		//Flag_0X8 = 0x8,
		UnkTrainer = 0x10,       // 100%
		ClassTrainer = 0x20,       // 100%
		ProfessionTrainer = 0x40,       // 100%
		Vendor = 0x80,       // 100%
		GeneralGoodsVendor = 0x100,       // 100%, general goods vendor
		FoodVendor = 0x200,       // 100%
		PoisonVendor = 0x400,       // guessed
		ReagentVendor = 0x800,       // 100%
		Armorer = 0x1000,       // 100%
		FlightMaster = 0x2000,       // 100%
		/// <summary>
		/// Makes the unit invisible when player is alive
		/// </summary>
		SpiritHealer = 0x4000,       // guessed
		/// <summary>
		/// Makes the unit invisible when player is alive
		/// </summary>
		SpiritGuide = 0x8000,       // guessed
		InnKeeper = 0x10000,       // 100%
		Banker = 0x20000,       // 100%
		Petitioner = 0x40000,       // 100% 0xC0000 = guild petitions, 0x40000 = arena team petitions
		TabardDesigner = 0x80000,       // 100%
		BattleMaster = 0x100000,       // 100%
		Auctioneer = 0x200000,       // 100%
		StableMaster = 0x400000,       // 100%
		GuildBanker = 0x800000,       // cause client to send 997 opcode
		/// <summary>
		/// Makes the client send CMSG_SPELLCLICK
		/// </summary>
		//Flag_0X1000000 = 0x1000000,       // cause client to send 1015 opcode
	}
}
