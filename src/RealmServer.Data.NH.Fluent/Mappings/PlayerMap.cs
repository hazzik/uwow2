using System;
using FluentNHibernate.Mapping;
using Hazzik.Objects;

namespace Hazzik.RealmServer.Data.NH.Fluent.Mappings {
	public class PlayerMap : ClassMap<Player> {
		public PlayerMap() {
			Table("Players");
			Not.LazyLoad();
			Id(p => p.Guid).Column("PlayerId").UnsavedValue("any").GeneratedBy.Native();
			Map(p => p.Name).Column("Name").Not.Nullable().Unique().Length(255);
		}
	}
}