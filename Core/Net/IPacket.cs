using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace UWoW {
	public interface IPacket {
		int Code { get; set; }
		int Size { get; }
		Stream GetStream();
	}
}
