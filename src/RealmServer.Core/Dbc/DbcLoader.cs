using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using Hazzik.IO;

namespace Hazzik.Dbc {
	public class DbcDataReader : IEnumerable<IDbcRow>, IDbcRow {
		private readonly int fieldCount;
		private readonly DbcField[] fields;

		private readonly int numRows;
		private readonly BinaryReader reader;
		private readonly int rowSize;
		private readonly IDictionary<int, string> strBlock = new Dictionary<int, string>();

		private readonly int strSize;
		private bool isOpen = true;
		private int readCount;

		public DbcDataReader(Stream stream) {
			reader = new BinaryReader(CreateInputStream(stream));
			if(new string(reader.ReadChars(4)) != "WDBC") {
				throw new Exception("Invalid DBC file.");
			}
			numRows = reader.ReadInt32();
			fieldCount = reader.ReadInt32();
			rowSize = reader.ReadInt32();
			strSize = reader.ReadInt32();

			fields = new DbcField[FieldCount];
			// its not true. each field must have its own size:)

			strBlock = LoadStringsBlock();
		}

		public bool HasRows {
			get { return numRows > 0; }
		}

		public int FieldCount {
			get { return fieldCount; }
		}

		public bool IsClosed {
			get { return !isOpen; }
		}

		private int StringsBlockOffset {
			get { return 20 + numRows * rowSize; }
		}

		#region IDbcRow Members

		public object this[int i] {
			get { return GetValue(i); }
		}

		public bool GetBoolean(int i) {
			return GetInt32(i) != 0;
		}

		public byte GetByte(int i) {
			return (byte)GetInt32(i);
		}

		public char GetChar(int i) {
			return (char)GetInt32(i);
		}

		public decimal GetDecimal(int i) {
			return fields[i].Int32;
		}

		public double GetDouble(int i) {
			return fields[i].Single;
		}

		public float GetFloat(int i) {
			return fields[i].Single;
		}

		public short GetInt16(int i) {
			return (short)GetInt32(i);
		}

		public int GetInt32(int i) {
			return fields[i].Int32;
		}

		public long GetInt64(int i) {
			return fields[i].Int32;
		}

		public string GetString(int i) {
			string result;
			strBlock.TryGetValue(GetInt32(i), out result);
			return result;
		}

		public object GetValue(int i) {
			return fields[i];
		}

		public int GetValues(object[] values) {
			if(values == null) {
				return 0;
			}
			int numCols = Math.Min(values.Length, FieldCount);
			for(int i = 0; i < numCols; i++) {
				values[i] = GetValue(i);
			}
			return numCols;
		}

		#endregion

		#region IEnumerable<IDbcRow> Members

		IEnumerator<IDbcRow> IEnumerable<IDbcRow>.GetEnumerator() {
			return GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator() {
			return GetEnumerator();
		}

		#endregion

		private IDictionary<int, string> LoadStringsBlock() {
			reader.BaseStream.Seek(StringsBlockOffset, SeekOrigin.Begin);
			var result = new Dictionary<int, string>();
			while(reader.BaseStream.Position - StringsBlockOffset < strSize) {
				result.Add((int)(reader.BaseStream.Position - StringsBlockOffset), reader.ReadCString());
			}
			return result;
		}

		private static Stream CreateInputStream(Stream stream) {
			if(stream.CanSeek) {
				return stream;
			}
			Stream input = new MemoryStream(1024);
			var buffer = new byte[1024];
			while(true) {
				int n = stream.Read(buffer, 0, 1024);
				if(n <= 0) {
					break;
				}
				input.Write(buffer, 0, n);
			}
			input.Seek(0, SeekOrigin.Begin);
			return input;
		}

		public void Close() {
			if(isOpen) {
				reader.Close();
			}
			isOpen = false;
		}

		public bool NextResult() {
			return readCount < numRows;
		}

		public bool Read() {
			if(!NextResult()) {
				return false;
			}
			reader.BaseStream.Seek(20 + readCount * rowSize, SeekOrigin.Begin);
			for(int i = 0; i < FieldCount; i++) {
				fields[i].Int32 = reader.ReadInt32();
			}
			readCount++;
			return true;
		}

		private IEnumerator<IDbcRow> GetEnumerator() {
			while(Read()) {
				yield return this;
			}
		}

		#region Nested type: DbcField

		[StructLayout(LayoutKind.Explicit)]
		private struct DbcField {
			[FieldOffset(0)] public Int32 Int32;
			[FieldOffset(0)] public Single Single;
		}

		#endregion
	}
}