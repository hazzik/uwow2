using System;
using Hazzik.Creatures;

namespace Hazzik.Data {
	public interface ICreatureTemplateRepository {
		CreatureTemplate FindById(uint id);
	}
}