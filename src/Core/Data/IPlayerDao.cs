using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hazzik.Data {
	public interface IPlayerDao : IDao<DbPlayer>, IGuided<DbPlayer>, INamed<DbPlayer> {
	}
}
