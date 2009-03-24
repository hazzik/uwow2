using System;
using System.IO;

namespace Hazzik.Objects.Update {
	public interface IUpdateBlock {
		bool IsEmpty { get; }
		UpdateType UpdateType { get; }
		void Write(BinaryWriter writer);
	}
}