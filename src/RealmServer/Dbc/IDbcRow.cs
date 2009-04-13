using System;

namespace Hazzik.Dbc {
	public interface IDbcRow {
		object this[int i] { get; }
		bool GetBoolean(int i);
		byte GetByte(int i);
		char GetChar(int i);
		decimal GetDecimal(int i);
		double GetDouble(int i);
		float GetFloat(int i);
		short GetInt16(int i);
		int GetInt32(int i);
		long GetInt64(int i);
		string GetString(int i);
		object GetValue(int i);
		int GetValues(object[] values);
	}
}