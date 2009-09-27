using System;
using FluentNHibernate.Mapping;
using Hazzik.Objects;

namespace Hazzik.RealmServer.Data.NH.Fluent.Mappings {
	public class PlayerMap : SubclassMap<Player> {
		public PlayerMap() {
			Table("Players");
			Not.LazyLoad();	
			KeyColumn("PlayerId");
			Map(p => p.Skin).Column("Skin");
			Map(p => p.Face).Column("Face");
			Map(p => p.HairStyle).Column("HairStyle");
			Map(p => p.HairColor).Column("HairColor");
			Map(p => p.FacialHair).Column("FacialHair");
		}
	}
}