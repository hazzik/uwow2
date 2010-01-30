using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Hazzik.Dbc {
    public class SkillLineAbilityRepository {
        private static readonly IDictionary<int, SkillLineAbility> entities = Load();

        private static IDictionary<int, SkillLineAbility> Load() {
            FileStream stream = File.OpenRead(@"DbFilesClient/SkillLineAbility.dbc");
            return new DbcDataReader(stream)
                .Select(row => new SkillLineAbility {
                    Id = row.GetInt32(0),
                    SkillId = row.GetInt32(1),
                    SpellId = row.GetInt32(2),
                    ReplacedBy = row.GetInt32(8),
                    Classes = row.GetInt32(4),
                    Min = row.GetInt32(7),
                    GreenToGray = row.GetInt32(10),
                    OrangeToYellow = row.GetInt32(11)
                })
                .ToDictionary(skillLineAbility => skillLineAbility.Id);
        }

        private IEnumerable<SkillLineAbility> FindAll() {
            return entities.Values;
        }

        public SkillLineAbility FindBySpellId(int spellId) {
            return FindAll().Where(sla => sla.SpellId == spellId).FirstOrDefault();
        }

        public IList<SkillLineAbility> FindBySkillId(int skillId) {
            return FindAll().Where(sla => sla.SkillId == skillId).ToList();
        }
    }
}