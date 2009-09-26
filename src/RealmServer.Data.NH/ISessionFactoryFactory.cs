using System;
using NHibernate;

namespace Hazzik.Data.NH {
	public interface ISessionFactoryFactory {
		ISessionFactory GetFactory();
	}
}