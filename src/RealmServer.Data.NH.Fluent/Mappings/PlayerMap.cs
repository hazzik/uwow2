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
			Map(p => p.Race).Column("Race").CustomType(typeof(Races));
			Map(p => p.Classe).Column("Class").CustomType(typeof(Classes));
			Map(p => p.Gender).Column("Gender").CustomType(typeof(GenderType));
			Map(p => p.Skin).Column("Skin");
			Map(p => p.Face).Column("Face");
			Map(p => p.HairStyle).Column("HairStyle");
			Map(p => p.HairColor).Column("HairColor");
			Map(p => p.FacialHair).Column("FacialHair");
			Map(p => p.Level).Column("Level");
			Map(p => p.DisplayId).Column("DisplayId");
			Map(p => p.NativeDisplayId).Column("NativeDisplayId");
		}
	}
}