using System;

namespace Hazzik.Data {
	public interface IDao<T> {
		void Delete(T entity);
		void Save(T entity);
		void SubmitChanges();
	}
}