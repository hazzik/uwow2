using System;
namespace Hazzik {
	public interface IAccountDao {
		DbAccount Create(string name);
		DbAccount GetByName(string name);

		void Save(DbAccount account);
		void Delete(DbAccount account);

		void SubmitChanges();
	}
}
