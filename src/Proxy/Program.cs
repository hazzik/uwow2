using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using UWoW.Helper;

namespace UWoW
{
	public class Singleton<T>
		where T : new() {
		static private T _instance = default(T);

		static public T GetInstance() {
			if(null == _instance) {
				_instance = new T();
			}
			return _instance;
		}
	}

	public class MainConfig : Singleton<MainConfig>
	{
		#region methods

		private Config conf = new Config();

		#endregion

		#region ctors
		public MainConfig()
		{
			conf.Load("main.conf");
		}
		#endregion

		#region Accessors

		public String RealmListHost
		{
			get { return conf.GetString("realmlist_host"); }
		}
		public Int32  RelamListPort
		{
			get { return conf.GetInt32("realmlist_port"); }
		}

		#endregion
	}

	class Program
	{
		static void Main(string[] args)
		{
			MainConfig conf = MainConfig.GetInstance();

			if (!Directory.Exists("raw"))
				Directory.CreateDirectory("raw");
			RLServer s1 = new RLServer(3724, conf.RealmListHost, 3724);
			RWServer s2 = new RWServer(3726, "213.248.123.4:3724");
		}
	}
}
