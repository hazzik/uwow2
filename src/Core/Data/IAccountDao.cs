using System;
namespace Hazzik.Data {
	public interface IAccountDao : IDao<DbAccount>, INamed<DbAccount> {
	}
}
