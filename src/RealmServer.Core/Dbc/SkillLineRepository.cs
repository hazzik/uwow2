using System.Collections.Generic;
using System.IO;
using System.Linq;
using Hazzik.Skills;

namespace Hazzik.Dbc
{
    public class SkillLineRepository
    {
        private static readonly IDictionary<int, SkillLine> entities = Load();

        private static IDictionary<int, SkillLine> Load()
        {
            FileStream stream = File.OpenRead(@"DbFilesClient/SkillLine.dbc");
            return new DbcDataReader(stream)
                .Select(row => new SkillLine
                                   {
                                       Id = row.GetInt32(0),
                                       Category = (SkillCategory) row.GetInt32(1)
                                   })
                .ToDictionary(skillLineAbility => skillLineAbility.Id);
        }

        public SkillLine FindById(int id)
        {
            return entities[id];
        }
    }
}