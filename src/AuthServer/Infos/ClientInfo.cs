using System;
using System.Net;

namespace Hazzik {
	public class ClientInfo {
		public VersionInfo VersionInfo { get; set; }
		public int TimeZone { get; set; }
		public IPAddress IP { get; set; }
		public string AccountName { get; set; }
	}
}