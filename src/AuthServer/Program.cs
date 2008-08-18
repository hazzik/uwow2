using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Reflection;
using System.Text;
using Hazzik.Helper;
using Hazzik.Net;

namespace Hazzik {
	public class Program {
		static void Main(string[] args) {
			var server = new AuthServer();
			server.Handler.AddAssembly(Assembly.GetExecutingAssembly());
			server.Handler.Load();
			server.Start();
		}
	}
}
