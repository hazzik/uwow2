using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Hazzik.Net;

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
