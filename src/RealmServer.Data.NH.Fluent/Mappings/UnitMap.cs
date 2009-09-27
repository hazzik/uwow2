using System;
using FluentNHibernate.Mapping;
using Hazzik.Objects;

namespace Hazzik.RealmServer.Data.NH.Fluent.Mappings {
	public class UnitMap : ClassMap<Unit> {
		public UnitMap() {
			Table("Units");
			Not.LazyLoad();
			Id(p => p.Guid).Column("UnitId").UnsavedValue("any").GeneratedBy.Native();
			Map(p => p.Name).Column("Name").Not.Nullable().Unique().Length(255);
			Map(p => p.Race).Column("Race").CustomType(typeof(Races));
			Map(p => p.Classe).Column("Class").CustomType(typeof(Classes));
			Map(p => p.Gender).Column("Gender").CustomType(typeof(GenderType));
			Map(p => p.Level).Column("Level");
			Map(p => p.DisplayId).Column("DisplayId");
			Map(p => p.NativeDisplayId).Column("NativeDisplayId");
			Map(p => p.Speed0).Column("Speed0");
			Map(p => p.Speed1).Column("Speed1");
			Map(p => p.Speed2).Column("Speed2");
			Map(p => p.Speed3).Column("Speed3");
			Map(p => p.Speed4).Column("Speed4");
			Map(p => p.Speed5).Column("Speed5");
			Map(p => p.Speed6).Column("Speed6");
			Map(p => p.TurnRate).Column("TurnRate");
		}
	}
}