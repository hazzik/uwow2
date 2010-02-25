using System;
using Hazzik.Annotations;
using Hazzik.Objects;

namespace Hazzik.Skills
{
    public class Skill
    {
        public static readonly Skill Empty = new Skill(null, SkillCap.Null);

        private readonly SkillCap cap;
        private readonly Player player;

        public Skill(Player player, [NotNull] SkillCap cap)
        {
            if (cap == null)
                throw new ArgumentNullException("cap");
            this.player = player;
            this.cap = cap;
        }

        public ushort Id { get; set; }

        public ushort Flags { get; set; }

        public ushort Value { get; set; }

        public ushort Modifier { get; set; }

        public ushort Modifier2 { get; set; }

        public override string ToString()
        {
            return "Skill: " + (SkillType) Id;
        }

        public ushort Cap()
        {
            return cap.ValueFor(player);
        }
    }
}