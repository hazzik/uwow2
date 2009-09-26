using System;

namespace Hazzik.Skills {
	public class Skill {
		public ushort Id { get; set; }

		public ushort Flags { get; set; }

		public ushort Value { get; set; }

		public ushort Cap { get; set; }

		public ushort Modifier { get; set; }

		public ushort Modifier2 { get; set; }

		public override string ToString() {
			return "Skill: " + (SkillType)Id;
		}
	}
}