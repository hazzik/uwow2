using System;
using Hazzik.Objects;

namespace Hazzik.Skills
{
    public class SkillLine
    {
        public int Id { get; set; }
        public SkillCategory Category { get; set; }

        private SkillCap GetCap()
        {
            switch (Category)
            {
                case SkillCategory.Attributes:
                    return SkillCap.Null;
                case SkillCategory.Weapon:
                    return SkillCap.Level;
                case SkillCategory.Class:
                    return SkillCap.Level;
                case SkillCategory.Armor:
                    return SkillCap.Level;
                case SkillCategory.Secondary:
                    return SkillCap.Level;
                case SkillCategory.Languages:
                    return SkillCap.Language;
                case SkillCategory.Profession:
                    return SkillCap.Level;
                case SkillCategory.Generic:
                    return SkillCap.Null;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private ushort MinValue()
        {
            if (Category == SkillCategory.Languages)
                return 300;
            return 1;
        }

        public Skill CreateSkill(Player player)
        {
            return new Skill(player, GetCap())
                       {
                           Id = (ushort) Id,
                           Value = MinValue(),
                       };
        }
    }
}