using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

namespace Hazzik {
	public class ClientInfo {
		public VersionInfo VersionInfo { get; set; }
		public int TimeZone { get; set; }
		public IPAddress IP { get; set; }
		public string AccountName { get; set; }
	}
}