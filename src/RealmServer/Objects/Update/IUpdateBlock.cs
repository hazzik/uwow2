using System;
using System.IO;

namespace Hazzik.Objects.Update {
	internal interface IUpdateBlock {
		bool IsEmpty { get; }
		UpdateType UpdateType { get; }
		void Write(BinaryWriter writer);
	}
}