using System;
using Hazzik.Objects;

namespace Hazzik.Creatures {
	public class Creature : Unit {
		private readonly CreatureTemplate _template;

		private Creature(CreatureTemplate template) {
			_template = template;
		}

		public CreatureTemplate Template {
			get { return _template; }
		}

		public static Creature Create(CreatureTemplate template) {
			if(template == null) {
				return null;
			}
			return new Creature(template) {
				Entry = template.Id,
				DisplayId = template.DisplayId
			};
		}
	}
}