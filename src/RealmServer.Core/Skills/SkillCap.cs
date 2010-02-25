using System;
using Hazzik.Objects;

namespace Hazzik.Skills
{
    public abstract class SkillCap
    {
        public static readonly SkillCap Null = new ConstantCap(0);
        public static readonly SkillCap Level = new LevelSkillCap();
        public static readonly SkillCap Mono = new ConstantCap(1);
        public static readonly SkillCap Language = new ConstantCap(300);


        public abstract ushort ValueFor(Player player);

        #region Nested type: ConstantCap

        private class ConstantCap : SkillCap
        {
            private readonly ushort value;

            public ConstantCap(ushort value)
            {
                this.value = value;
            }

            public override ushort ValueFor(Player player)
            {
                return value;
            }
        }

        #endregion

        #region Nested type: LevelSkillCap

        private class LevelSkillCap : SkillCap
        {
            public override ushort ValueFor(Player player)
            {
                return (ushort) (player.Level*5);
            }
        }

        #endregion
    }
}