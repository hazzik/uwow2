using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using UWoW.Net;
using Helper;
using System.Xml.Serialization;

namespace UWoW {
	[Serializable]
	public class AddonInfo {
		[XmlAttribute]
		public string Name { get; set; }

		[XmlAttribute]
		public ulong Crc { get; set; }

		[XmlAttribute]
		public byte Status { get; set; }

		public AddonInfo() {

		}

		public AddonInfo(string name)
			: this(name, 0) {
		}

		public AddonInfo(string name, ulong crc)
			: this(name, crc, 2) {
		}

		private AddonInfo(string name, ulong crc, byte status) {
			Name = name;
			Crc = crc;
			Status = status;
		}
	}
}
