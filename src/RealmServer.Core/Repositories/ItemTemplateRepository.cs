using Hazzik.Items;
using Hazzik.Items.Templates;

namespace Hazzik.Repositories {
	public class ItemTemplateRepository {
		public static ItemTemplate FindById(uint id) {
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
	}
}