using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using Hazzik.Helper;
using Hazzik.Net;

namespace Hazzik {
	public class Program {
		static ServerBase _authServer;
		static void Main(string[] args) {
			_authServer = new AuthServer();
		}
	}
}
