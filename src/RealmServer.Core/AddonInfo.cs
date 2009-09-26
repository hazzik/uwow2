using System;
using System.Xml.Serialization;

namespace Hazzik {
	[Serializable]
	public class AddonInfo {
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

		[XmlAttribute]
		public string Name { get; set; }

		[XmlAttribute]
		public ulong Crc { get; set; }

		[XmlAttribute]
		public byte Status { get; set; }
	}
}