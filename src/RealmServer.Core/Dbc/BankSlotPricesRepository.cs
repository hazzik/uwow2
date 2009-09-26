using System;
using System.Collections.Generic;
using System.IO;

namespace Hazzik.Dbc {
	public class BankSlotPricesRepository {
		private static readonly IDictionary<uint, uint> Entities = Load();

		private static IDictionary<uint, uint> Load() {
			FileStream stream = File.OpenRead(@"DbFilesClient/BankBagSlotPrices.dbc");
			var dbcReader = new DbcDataReader(stream);
			var result = new Dictionary<uint, uint>();
			foreach(IDbcRow row in dbcReader) {
				result.Add((uint)row.GetInt32(0), (uint)row.GetInt32(1));
			}
			return result;
		}

		public static uint GetCost(uint slotId) {
			uint cost;
			Entities.TryGetValue(slotId, out cost);
			return cost;
		}
	}
}