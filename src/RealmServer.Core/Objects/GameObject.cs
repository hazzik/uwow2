using System;
using Hazzik.GameObjects;

namespace Hazzik.Objects {
	public partial class GameObject : Positioned {
		private readonly GameObjectTemplate _template;

		public GameObject(GameObjectTemplate template) {
			_template = template;
			Entry = _template.Id;
			GameObjectType = _template.Type;
			DisplayId = _template.DisplayId;
			ScaleX = _template.ScaleX;
			Type |= ObjectTypes.GameObject;
		}

		public override ObjectTypeId TypeId {
			get { return ObjectTypeId.GameObject; }
		}

		public virtual UInt64 Rotation { get; set; }
	}
}