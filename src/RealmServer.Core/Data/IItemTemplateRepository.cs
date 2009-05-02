using System;
using Hazzik.Items;

namespace Hazzik.Data {
	public interface IItemTemplateRepository {
		ItemTemplate FindById(uint id);
	}
}