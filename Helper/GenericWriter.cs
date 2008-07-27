using System.IO;
using System;

namespace Helper
{
	public class GenericWriter : BinaryWriter, IGenWriter
	{
		public GenericWriter(string fname)
			: base(new FileStream(fname, FileMode.Create, FileAccess.Write))
		{
		}
		public GenericWriter(Stream output)
			: base(output)
		{
		}
		#region IGenWriter Members

		void IGenWriter.WriteString0(string a)
		{
			if (a == null) a = string.Empty;

			for (int i = 0; i < a.Length; i++)
				Write((byte)a[i]);
			Write((byte)0);
		}

		#endregion
	}
}

