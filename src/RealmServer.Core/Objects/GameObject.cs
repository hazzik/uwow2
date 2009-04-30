using System;
using Hazzik.GameObjects;

namespace Hazzik.Objects {
	public partial class GameObject : Positioned {
		private readonly GameObjectTemplate _template;

		private GameObject(GameObjectTemplate template) {
			Type |= ObjectTypes.GameObject;
			_template = template;
		}

		public GameObjectTemplate Template {
			get { return _template; }
		}

		public override ObjectTypeId TypeId {
			get { return ObjectTypeId.GameObject; }
		}

		public virtual UInt64 Rotation { get; set; }

		public static GameObject Create(GameObjectTemplate template) {
			if(template == null) {
				return null;
			}
			return new GameObject(template) {
				Entry = template.Id,
				GameObjectType = template.Type,
				DisplayId = template.DisplayId,
				ScaleX = template.ScaleX
			};
		}
	}
}