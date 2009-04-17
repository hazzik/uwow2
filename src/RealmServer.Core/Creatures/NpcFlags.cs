using System;

namespace Hazzik.Creatures {
	[Flags]
	public enum NpcFlags : uint {
		None = 0,
		Gossip = 1,	//SpiritHealer = 1, // gossip\talk 
		QuestGiver = 2,	//Dialog = 2,		// questgiver (CMSG_QUESTGIVER_HELLO, ?)
		Vendor = 4,	//Vendor = 4,		// vendor (CMSG_LIST_INVENTORY, SMSG_LIST_INVENTORY)
		TaxiVendor = 8,	//Taxi = 8,			// taxi (CMSG_TAXIQUERYAVAILABLENODES, SMSG_SHOWTAXINODES)
		Trainer = 16,	//Trainer = 16,		// trainer (CMSG_TRAINER_LIST, SMSG_TRAINER_LIST)
		SpiritHealer = 32,	//Healer = 32,		// SpiritHealer (CMSG_BINDER_ACTIVATE, ?)
		Guard = 64,	//BattleFieldSpiritHealer = 64, //no opcodes for 1.8??? only indication?
		Innkeeper = 128,	//InnKeeper = 128,	// 
		Banker = 256,	//Banker = 256,		// Banker (CMSG_BANKER_ACTIVATE, SMSG_SHOW_BANK)
		Petitioner = 512,	//Petition = 512,	// GuildMaster (CMSG_PETITION_SHOWLIST, SMSG_PETITION_SHOWLIST)
		TabardVendor = 1024,	//Tabard = 1024,	// TabardVendor (MSG_TABARDVENDOR_ACTIVATE)
		BattlefieldPerson = 2048,	//BattleMaster = 2048, // Battlefield Person (CMSG_BATTLEFIELD_LIST, SMSG_BATTLEFIELD_LIST)
		Auctioneer = 4096,	//Auctionner = 4096, //MSG_AUCTION_HELLO
		Stable = 8192,	//StableMaster = 8192, //MSG_LIST_STABLED_PETS
		Armorer = 16384,						//no opcodes for 1.8???
	}
}
