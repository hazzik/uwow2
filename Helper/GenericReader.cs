using System.IO;
using System;
using System.Text;

namespace Helper
{
	public class GenericReader : BinaryReader, IGenReader
	{
		#region Constructors

		public GenericReader(string fname)
			: base(new FileStream(fname, FileMode.Open, FileAccess.Read))
		{
		}

		public GenericReader(Stream input)
			: base(input)
		{
		}

		#endregion

		#region IGenReader Members

		string IGenReader.ReadString0()
		{
			byte b;
			StringBuilder s = new StringBuilder();
			while ((b = ReadByte()) != 0)
				s.Append((char)b);
			return s.ToString();
		}

		#endregion

	}
}

