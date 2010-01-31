using System;
using Hazzik.GameObjects;

namespace Hazzik.Data.NH {
    public class NHGameObjectTemplateRepository : NHDao<GameObjectTemplate>, IGameObjectTemplateRepository {
        #region IGameObjectTemplateRepository Members

        public GameObjectTemplate FindById(uint id) {
            return session.QueryOver<GameObjectTemplate>()
                .Where(x => x.Id == id)
                .SingleOrDefault();
        }

        #endregion
    }
}