using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Hazzik {
	[Serializable]
	public class VersionInfo {
		[XmlAttribute]
		public string ClientTag { get; set; }
		[XmlIgnore]
		public Version Version { get; set; }
		[XmlAttribute(AttributeName = "Version")]
		public string VersionString {
			get { return Version.ToString(4); }
			set { Version = new Version(value); }
		}
		[XmlAttribute]
		public string Platform { get; set; }
		[XmlAttribute]
		public string OS { get; set; }
		[XmlAttribute]
		public string Locale { get; set; }
	}
}
