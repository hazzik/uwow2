using System;

namespace Hazzik.Data {
	public interface IDaoFactory {
		IAccountDao GetAccountDao();
		IPlayerDao GetPlayerDao();
	}
}