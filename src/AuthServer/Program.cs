using System;
using System.Collections.Generic;
using System.Text;
using Hazzik.Helper;
using System.IO;
using System.Net;
using Hazzik.Net;

namespace Hazzik {
	public class Program {
		static ServerBase _authServer;
		static ServerBase _worldServer;
		static void Main(string[] args) {
			_authServer = new AuthServer();
		}
	}
}
