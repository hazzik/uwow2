using System;
using System.IO;

namespace Helper
{
	public interface IGenWriter
	{
		void Close();

		void Write(bool a);
		void Write(byte a);
		void Write(sbyte a);
		void Write(char a);
		void Write(short a);
		void Write(ushort a);
		void Write(int a);
		void Write(uint a);
		void Write(long a);
		void Write(ulong a);

		void Write(float a);
		void Write(double a);
		void Write(decimal a);
		void Write(string a);
		void WriteString0(string a);

		void Write(byte[] a);
		void Write(char[] a);

		Stream BaseStream { get;}
	}
}
