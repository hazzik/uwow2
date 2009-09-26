using System;
using FluentNHibernate.Mapping;
using Hazzik.GameObjects;

namespace Hazzik.RealmServer.Data.NH.Fluent.Mappings {
	public class GameObjectTemplateMap : ClassMap<GameObjectTemplate> {
		public GameObjectTemplateMap() {
			Table("GameObjectTemplates");
			Not.LazyLoad();

			Id(t => t.Id).Column("Id").UnsavedValue("any");
			Map(t => t.Type).Column("Type").CustomType(typeof(GameObjectType));
			Map(t => t.DisplayId).Column("DisplayId");
			Map(t => t.Name).Column("Name");
			Map(t => t.Field0).Column("Field0");
			Map(t => t.Field1).Column("Field1");
			Map(t => t.Field2).Column("Field2");
			Map(t => t.Field3).Column("Field3");
			Map(t => t.Field4).Column("Field4");
			Map(t => t.Field5).Column("Field5");
			Map(t => t.Field6).Column("Field6");
			Map(t => t.Field7).Column("Field7");
			Map(t => t.Field8).Column("Field8");
			Map(t => t.Field9).Column("Field9");
			Map(t => t.Field10).Column("Field10");
			Map(t => t.Field11).Column("Field11");
			Map(t => t.Field12).Column("Field12");
			Map(t => t.Field13).Column("Field13");
			Map(t => t.Field14).Column("Field14");
			Map(t => t.Field15).Column("Field15");
			Map(t => t.Field16).Column("Field16");
			Map(t => t.Field17).Column("Field17");
			Map(t => t.Field18).Column("Field18");
			Map(t => t.Field19).Column("Field19");
			Map(t => t.Field20).Column("Field20");
			Map(t => t.Field21).Column("Field21");
			Map(t => t.Field22).Column("Field22");
			Map(t => t.Field23).Column("Field23");
			Map(t => t.ScaleX).Column("ScaleX");
		}
	}
}