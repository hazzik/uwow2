namespace Hazzik.Data {
	public interface IAccountRepository : IDao<Account>, IFindByName<Account> {
	}
}