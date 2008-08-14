using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using UWoW.Helper;

namespace UWoW {
	public class LazySingleton<T>
		where T : new() {
		private sealed class Nested {
			public static readonly T Instance = new T();
		}
		static public T Instance {
			get { return Nested.Instance; }
		}
	}

	public class MainConfig : LazySingleton<MainConfig> {
		#region methods

		private Config conf = new Config();

		#endregion

		#region ctors
		public MainConfig() {
			conf.Load("main.conf");
		}
		#endregion

		#region Accessors

		public String RealmListHost {
			get { return conf.GetString("realmlist_host"); }
		}
		public Int32 RelamListPort {
			get { return conf.GetInt32("realmlist_port"); }
		}

		#endregion
	}

	class Program {
		static void Main(string[] args) {
			MainConfig conf = MainConfig.Instance;

			if(!Directory.Exists("raw"))
				Directory.CreateDirectory("raw");
			RLServer s1 = new RLServer(3724, conf.RealmListHost, 3724);
			RWServer s2 = new RWServer(3726, "213.248.123.4:3724");
		}
	}
}
