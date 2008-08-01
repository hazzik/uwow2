using System;
using System.Collections.Generic;
using System.Text;
using Helper;
using System.IO;
using System.Net;
using UWoW.Net;

namespace UWoW {
	public class Program {
		static ServerBase _authServer;
		static ServerBase _worldServer;
		static public AddonManager addonManager = new AddonManager();
		static void Main(string[] args) {
			addonManager.Load("addons.xml");
			_authServer = new AuthServer();
			_worldServer = new WorldServer();
		}
	}
}
