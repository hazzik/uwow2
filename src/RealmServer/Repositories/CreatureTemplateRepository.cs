using System;
using Hazzik.Creatures;
using Hazzik.Objects.Templates;

namespace Hazzik.Repositories {
	public class CreatureTemplateRepository {
		public static CreatureTemplate FindById(uint id) {
			switch(id) {
			case 647:
				return new Creature647();
			}
			return null;
		}
	}
}