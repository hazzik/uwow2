using System;
using System.IO;
using Hazzik.Objects;

namespace Hazzik {
	public interface IUpdateBlock {
		bool IsEmpty { get; }
		UpdateType UpdateType { get; }
		void Write(BinaryWriter writer);
	}
}