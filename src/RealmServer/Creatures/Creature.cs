using Hazzik.Objects;

namespace Hazzik.Creatures {
	public class Creature : Unit {
		private readonly CreatureTemplate _template;

		public Creature(CreatureTemplate template) {
			_template = template;
			Entry = _template.Id;
			DisplayId = _template.DisplayId;
		}

		public CreatureTemplate Template {
			get { return _template; }
		}
	}
}