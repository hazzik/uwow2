using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Hazzik.Dbc {
    public class BankSlotPricesRepository {
        private static readonly IDictionary<uint, uint> entities = Load();

        private static IDictionary<uint, uint> Load() {
            FileStream stream = File.OpenRead(@"DbFilesClient/BankBagSlotPrices.dbc");
            return new DbcDataReader(stream)
                .ToDictionary(row => (uint)row.GetInt32(0),
                              row => (uint)row.GetInt32(1));
        }

        public static uint GetCost(uint slotId) {
            uint cost;
            entities.TryGetValue(slotId, out cost);
            return cost;
        }
    }
}