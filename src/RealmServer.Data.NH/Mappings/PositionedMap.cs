using System;
using FluentNHibernate.Mapping;
using Hazzik.Objects;

namespace Hazzik.Data.NH.Mappings {
	public class PositionedMap : ClassMap<Positioned> {
		public PositionedMap() {
			Table("Positioneds");
			Not.LazyLoad();
			Id(p => p.Guid).Column("PositionedId").UnsavedValue("any").GeneratedBy.Native();
			Map(p => p.MapId).Column("MapId");
			Map(p => p.ZoneId).Column("ZoneId");
			Map(p => p.PosX).Column("PosX");
			Map(p => p.PosY).Column("PosY");
			Map(p => p.PosZ).Column("PosZ");
			Map(p => p.Facing).Column("Facing");
		}
	}
}