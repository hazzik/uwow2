using System;
using System.IO;

namespace Helper
{
	public interface IGenReader
	{
		void Close();
		bool ReadBoolean();
		byte ReadByte();
		sbyte ReadSByte();
		char ReadChar();
		short ReadInt16();
		ushort ReadUInt16();
		int ReadInt32();
		uint ReadUInt32();
		long ReadInt64();
		ulong ReadUInt64();
		float ReadSingle();
		double ReadDouble();
		decimal ReadDecimal();
		string ReadString();
		string ReadString0(); // read c strings

		char[] ReadChars(int p);
		byte[] ReadBytes(int p);

		Stream BaseStream { get;}
	}
}
