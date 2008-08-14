using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Hazzik {
	public interface IPacket {
		int Code { get; set; }
		int Size { get; }
		Stream GetStream();
		BinaryReader GetReader();
		BinaryWriter GetWriter();
	}
}
