using System;
namespace Hazzik {
	public interface IAccountDao : IDao<DbAccount>, INamed<DbAccount> {
	}
}
