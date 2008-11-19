using System;

namespace Hazzik.Data {
	public interface IAccountDao : IDao<Account>, INamed<Account> {
	}
}