using System;
using Hazzik.Data.Fake.Templates;
using Hazzik.Items;

namespace Hazzik.Data.Fake {
	public class FakeItemTemplateRepository : IItemTemplateRepository {
		#region IItemTemplateRepository Members

		public ItemTemplate FindById(uint id) {
			switch(id) {
			case 9936:
				return new Abjurer_sBoots9936();
			case 3289:
				return new AncestralBoots3289();
			case 23772:
				return new FelIronShells23772();
			case 857:
				return new LargeRedSack857();
			case 9943:
				return new AbjurerSRobe9943();
			default:
				return new AuchenaiKey30633();
			}
		}

		#endregion
	}
}