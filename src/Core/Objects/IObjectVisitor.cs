using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hazzik.Objects {
	public interface IObjectVisitor {
		void Visit(WorldObject obj);
		void Visit(Item obj);
		void Visit(Container obj);
		void Visit(Unit obj);
		void Visit(Player obj);
		void Visit(GameObject obj);
		void Visit(DynamicObject obj);
		void Visit(Corpse obj);
	}
}
