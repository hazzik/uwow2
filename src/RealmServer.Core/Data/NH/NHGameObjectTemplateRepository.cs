using System;
using System.Linq;
using Hazzik.GameObjects;
using NHibernate.Criterion;

namespace Hazzik.Data.NH {
	public class NHGameObjectTemplateRepository : NHDao<GameObjectTemplate>, IGameObjectTemplateRepository {
		#region IGameObjectTemplateRepository Members

		public GameObjectTemplate FindById(uint id) {
			var criteria = CreateCriteria();
			criteria.Add(Restrictions.Eq("Id", id));
			return criteria.List<GameObjectTemplate>().FirstOrDefault();
		}

		#endregion
	}
}