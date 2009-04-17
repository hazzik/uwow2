using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;

namespace Hazzik.Dbc {
	public class DbcDataReader : IEnumerable<IDbcRow>, IDbcRow {
		private readonly int _fieldCount;
		private readonly DbcField[] _fields;

		private readonly int _numRows;
		private readonly BinaryReader _reader;
		private readonly int _rowSize;
		private readonly IDictionary<int, string> _strBlock = new Dictionary<int, string>();

		private readonly int _strSize;
		private bool _isOpen = true;
		private int _readCount;

		public DbcDataReader(Stream stream) {
			_reader = new BinaryReader(CreateInputStream(stream));
			if(new string(_reader.ReadChars(4)) != "WDBC") {
				throw new Exception("Invalid DBC file.");
			}
			_numRows = _reader.ReadInt32();
			_fieldCount = _reader.ReadInt32();
			_rowSize = _reader.ReadInt32();
			_strSize = _reader.ReadInt32();

			_fields = new DbcField[FieldCount];
			// its not true. each field must have its own size:)

			_strBlock = LoadStringsBlock();
		}

		public bool HasRows {
			get { return _numRows > 0; }
		}

		public int FieldCount {
			get { return _fieldCount; }
		}

		public bool IsClosed {
			get { return !_isOpen; }
		}

		private int StringsBlockOffset {
			get { return 20 + _numRows * _rowSize; }
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
			return _fields[i].Int32;
		}

		public double GetDouble(int i) {
			return _fields[i].Single;
		}

		public float GetFloat(int i) {
			return _fields[i].Single;
		}

		public short GetInt16(int i) {
			return (short)GetInt32(i);
		}

		public int GetInt32(int i) {
			return _fields[i].Int32;
		}

		public long GetInt64(int i) {
			return _fields[i].Int32;
		}

		public string GetString(int i) {
			string result;
			_strBlock.TryGetValue(GetInt32(i), out result);
			return result;
		}

		public object GetValue(int i) {
			return _fields[i];
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
			_reader.BaseStream.Seek(StringsBlockOffset, SeekOrigin.Begin);
			var result = new Dictionary<int, string>();
			while(_reader.BaseStream.Position - StringsBlockOffset < _strSize) {
				result.Add((int)(_reader.BaseStream.Position - StringsBlockOffset), _reader.ReadCString());
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
			if(_isOpen) {
				_reader.Close();
			}
			_isOpen = false;
		}

		public bool NextResult() {
			return _readCount < _numRows;
		}

		public bool Read() {
			if(!NextResult()) {
				return false;
			}
			_reader.BaseStream.Seek(20 + _readCount * _rowSize, SeekOrigin.Begin);
			for(int i = 0; i < FieldCount; i++) {
				_fields[i].Int32 = _reader.ReadInt32();
			}
			_readCount++;
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