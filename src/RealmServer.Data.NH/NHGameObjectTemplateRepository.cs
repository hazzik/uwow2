using System;
using Hazzik.GameObjects;
using NHibernate;
using NHibernate.Criterion;

namespace Hazzik.Data.NH {
    public class NHGameObjectTemplateRepository : NHDao<GameObjectTemplate>, IGameObjectTemplateRepository {
        #region IGameObjectTemplateRepository Members

        public GameObjectTemplate FindById(uint id) {
            ICriteria criteria = CreateCriteria();
            criteria.Add(Restrictions.Eq("Id", id));
            return criteria.UniqueResult<GameObjectTemplate>();
        }

        #endregion
    }
}