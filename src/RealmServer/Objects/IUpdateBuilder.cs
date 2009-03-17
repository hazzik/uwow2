using System;
using System.IO;

namespace Hazzik.Objects {
	public interface IUpdateBuilder {
		bool IsChanged { get; }
		void Write(BinaryWriter writer);
	}
}