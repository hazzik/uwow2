namespace Hazzik {
	public enum AccountDataType
	{
		GlobalConfigCache = 0,                    // 0x01 g
		PerCharacterConfigCache = 1,                    // 0x02 p
		GlobalBindingsCache = 2,                    // 0x04 g
		PerCharacterBindingsCache = 3,                    // 0x08 p
		GlobalMacrosCache = 4,                    // 0x10 g
		PerCharacterMacrosCache = 5,                    // 0x20 p
		PerCharacterLayoutCache = 6,                    // 0x40 p
		PerCharacterChatCache = 7,                    // 0x80 p
	}
}