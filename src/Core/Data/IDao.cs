using System;
namespace Hazzik.Data {
	public interface IDao<T> {
		T Create(string name);
		void Delete(T entity);
		void Save(T entity);
		void SubmitChanges();
	}
}
