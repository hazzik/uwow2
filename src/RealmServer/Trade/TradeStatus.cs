using System;

namespace Hazzik.Trade {
	public enum TradeStatus {
		PlayerBusy = 0x00,
		Proposed = 0x01,
		Initiated = 0x02,
		Cancelled = 0x03,
		Accepted = 0x04,
		AlreadyTrading = 0x05,
		PlayerNotFound = 0x06,
		StateChanged = 0x07,
		Complete = 0x08,
		Unaccepted = 0x09,
		TooFarAway = 0x0A,
		WrongFaction = 0x0B,
		Failed = 0x0C,
		TargetDead = 0x0D,
		Petition = 0x0E,
		PlayerIgnored = 0x0F,
	}
}