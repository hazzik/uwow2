using System;
using Hazzik.Creatures;
using Hazzik.Data.Fake.Templates;

namespace Hazzik.Data.Fake {
	public class FakeCreatureTemplateRepository : ICreatureTemplateRepository {
		#region ICreatureTemplateRepository Members

		public CreatureTemplate FindById(uint id) {
			switch(id) {
			case 647:
				return new Creature647();
			}
			return null;
		}

		#endregion
	}
}