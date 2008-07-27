using System;
using System.Collections.Generic;
using System.Text;
using Helper;
using System.IO;
using System.Net;
using UWoW.Net;

namespace UWoW {
	public class Program {
		static AServer RLServer;
		static void Main(string[] args) {
			RLServer = new RLServer();
		}
	}
}
