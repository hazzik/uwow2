using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

namespace UWoW {
	public class ClientInfo {
		private VersionInfo _versionInfo = new VersionInfo();
		public VersionInfo VersionInfo {
			get { return _versionInfo; }
			set { _versionInfo = value; }
		}

		private int _timeZone;
		public int TimeZone {
			get { return _timeZone; }
			set { _timeZone = value; }
		}

		private IPAddress _ip;
		public IPAddress IP {
			get { return _ip; }
			set { _ip = value; }
		}

		private string _accountName;
		public string AccountName {
			get { return _accountName; }
			set { _accountName = value; }
		}
	}
}