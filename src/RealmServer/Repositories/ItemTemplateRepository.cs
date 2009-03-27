using Hazzik.Objects;
using Hazzik.Objects.Templates;

namespace Hazzik.Repositories {
	public class ItemTemplateRepository {
		public static ItemTemplate FindById(uint id) {
			switch(id) {
			case 9936:
				return new Abjurer_sBoots9936();
			case 3289:
				return new AncestralBoots3289();
			default:
				return new FelIronShells23772();
			}
		}
	}
}