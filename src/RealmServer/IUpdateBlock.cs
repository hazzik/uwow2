using System.IO;

namespace Hazzik {
	public interface IUpdateBlock {
		bool IsChanged { get; }
		void Write(BinaryWriter writer);
	}
}