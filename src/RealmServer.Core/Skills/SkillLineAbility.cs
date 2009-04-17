using System;

namespace Hazzik.Dbc {
	public class SkillLineAbility {
		public int Id { get; set; }

		public int SkillId { get; set; }

		public int SpellId { get; set; }

		public int ReplacedBy { get; set; }

		public int Classes { get; set; }

		public int Min { get; set; }

		public int GreenToGray { get; set; }

		public int OrangeToYellow { get; set; }
	}
}