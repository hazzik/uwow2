using System;
using FluentNHibernate.Mapping;

namespace Hazzik.RealmServer.Data.NH.Fluent.Mappings {
	public class AccountMap : ClassMap<Account> {
		public AccountMap() {
			Table("Accounts");
			Not.LazyLoad();
			Id(a => a.Id).Column("AccountId").GeneratedBy.Native();
			Map(a => a.Name).Column("Name").Not.Nullable().Unique().Length(255);
			Map(a => a.PasswordSalt).Column("PasswordSalt");
			Map(a => a.PasswordVerifier).Column("PasswordVerifier");
			Map(a => a.SessionKey).Column("SessionKey");
			HasMany(a => a.Players).KeyColumn("AccountId").Not.LazyLoad().Cascade.AllDeleteOrphan();
		}
	}
}