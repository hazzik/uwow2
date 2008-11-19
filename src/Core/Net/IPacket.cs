using System;
using System.IO;

namespace Hazzik.Net {
	public interface IPacket {
		int Code { get; }
		int Size { get; }
		Stream GetStream();
		BinaryReader CreateReader();
		BinaryWriter CreateWriter();
		void WriteHead(Stream stream);
		void WriteBody(Stream stream);
	}
}