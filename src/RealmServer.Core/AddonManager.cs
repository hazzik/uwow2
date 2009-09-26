using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;

namespace Hazzik {
	public class AddonManager {
		private static readonly XmlSerializer _serializer = new XmlSerializer(typeof(List<AddonInfo>));
		private static AddonManager _instance;

		public List<AddonInfo> AddonInfos = new List<AddonInfo>();

		public static AddonManager Instance {
			get {
				if(_instance == null) {
					_instance = new AddonManager();
				}
				return _instance;
			}
		}

		public AddonInfo this[string name] {
			get {
				return (from addon in AddonInfos
				        where addon.Name == name
				        select addon).FirstOrDefault();
			}
			set {
				AddonInfo addon = this[name];
				if(addon != null) {
					AddonInfos.Remove(addon);
				}
				AddonInfos.Add(value);
			}
		}

		public void Load(string fileName) {
			using(FileStream file = new FileInfo(fileName).Open(FileMode.OpenOrCreate, FileAccess.Read)) {
				AddonInfos = (List<AddonInfo>)_serializer.Deserialize(file);
			}
		}

		public void Save(string filename) {
			using(FileStream file = new FileInfo(filename).Open(FileMode.Create, FileAccess.Write)) {
				_serializer.Serialize(file, AddonInfos);
			}
		}
	}
}