using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hazzik.Data {
	public interface IDaoFactory {
		IAccountDao GetAccountDao();
		IPlayerDao GetPlayerDao();
	}
}
