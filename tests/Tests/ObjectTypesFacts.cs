using System;
using Hazzik.Objects;
using Xunit;

namespace Tests {
	public class ObjectTypesFacts {
		[Fact]
		public void Equal() {
			Assert.Equal((int)ObjectTypes.Object, 1 << (int)ObjectTypeId.Object);
			Assert.Equal((int)ObjectTypes.Item, 1 << (int)ObjectTypeId.Item);
			Assert.Equal((int)ObjectTypes.Container, 1 << (int)ObjectTypeId.Container);
			Assert.Equal((int)ObjectTypes.Unit, 1 << (int)ObjectTypeId.Unit);
			Assert.Equal((int)ObjectTypes.Player, 1 << (int)ObjectTypeId.Player);
			Assert.Equal((int)ObjectTypes.GameObject, 1 << (int)ObjectTypeId.GameObject);
			Assert.Equal((int)ObjectTypes.DynamicObject, 1 << (int)ObjectTypeId.DynamicObject);
			Assert.Equal((int)ObjectTypes.Corpse, 1 << (int)ObjectTypeId.Corpse);
		}
	}
}