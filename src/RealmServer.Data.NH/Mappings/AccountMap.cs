using System;
using FluentNHibernate.Mapping;

namespace Hazzik.Data.NH.Mappings {
    public class AccountMap : ClassMap<Account> {
        public AccountMap() {
            Table("Accounts");
            Not.LazyLoad();

            Id(a => a.Id).Column("AccountId").GeneratedBy.Native();
            Map(a => a.Name).Column("Name").Not.Nullable().Unique().Length(255);
            Map(a => a.PasswordSalt).Column("PasswordSalt");
            Map(a => a.PasswordVerifier).Column("PasswordVerifier");
            Map(a => a.SessionKey).Column("SessionKey");
            Map(a => a.Expansion).Column("Expansion");
            
            HasMany(a => a.Players)
                .KeyColumn("AccountId")
                .Not.LazyLoad()
                .Cascade.AllDeleteOrphan()
                .Access.ReadOnlyPropertyThroughCamelCaseField();

            HasMany(a => a.Datas)
                .Table("AccountDatas")
                .KeyColumn("AccountId")
                .Not.LazyLoad()
                .Cascade.AllDeleteOrphan()
                .Access.ReadOnlyPropertyThroughCamelCaseField()
                .Component(a => {
                               a.Map(ad => ad.Guid).Column("Guid");
                               a.Map(ad => ad.Type).Column("Type").CustomType<AccountDataType>();
                               a.Map(ad => ad.Time).Column("Time");
                               a.Map(ad => ad.Data).Column("Data");
                           });
        }
    }
}