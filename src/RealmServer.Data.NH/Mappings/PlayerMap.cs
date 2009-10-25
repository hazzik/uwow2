using System;
using FluentNHibernate.Mapping;
using Hazzik.Objects;
using JetBrains.Annotations;

namespace Hazzik.Data.NH.Mappings {
	[UsedImplicitly]
	public class PlayerMap : ClassMap<Player> {
		public PlayerMap() {
			Table("Players");
			Not.LazyLoad();	

			//Positioned
			Id(p => p.Guid).Column("PlayerId").UnsavedValue("any").GeneratedBy.Native();
			Map(p => p.MapId).Column("MapId");
			Map(p => p.ZoneId).Column("ZoneId");
			Map(p => p.PosX).Column("PosX");
			Map(p => p.PosY).Column("PosY");
			Map(p => p.PosZ).Column("PosZ");
			Map(p => p.Facing).Column("Facing");

			//Unit
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

			//Player
			Map(p => p.Skin).Column("Skin");
			Map(p => p.Face).Column("Face");
			Map(p => p.HairStyle).Column("HairStyle");
			Map(p => p.HairColor).Column("HairColor");
			Map(p => p.FacialHair).Column("FacialHair");
		}
	}
}