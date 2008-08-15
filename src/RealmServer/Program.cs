using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using Hazzik.Net;
using Hazzik.Attributes;

namespace Hazzik {
	class Program {
		static void Main(string[] args) {
			var server = new WorldServer();
			server.Handler.AddAssembly(Assembly.GetExecutingAssembly());
			server.Handler.Load();
			server.Start();
		}
	}
}
