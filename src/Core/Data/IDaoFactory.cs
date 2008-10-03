using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hazzik {
	public interface IDaoFactory {
		IAccountDao GetAccountDao();
	}
}
