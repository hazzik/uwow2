using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.IO;
using System.Collections;

namespace Hazzik {
	public class AddonManager {
		static XmlSerializer _serializer = new XmlSerializer(typeof(List<AddonInfo>));
		List<AddonInfo> _addonInfos = new List<AddonInfo>();

		public void Load(string fileName) {
			using(var file = new FileInfo(fileName).Open(FileMode.OpenOrCreate, FileAccess.Read)) {
				_addonInfos = (List<AddonInfo>)_serializer.Deserialize(file);
			}
		}

		public void Save(string filename) {
			using(var file = new FileInfo(filename).Open(FileMode.Create, FileAccess.Write)) {
				_serializer.Serialize(file, _addonInfos);
			}
		}

		public AddonInfo this[string name] {
			get {
				return (from addon in _addonInfos
						  where addon.Name == name
						  select addon).FirstOrDefault();
			}
			set {
				var addon = this[name];
				if(addon != null) {
					_addonInfos.Remove(addon);
				}
				_addonInfos.Add(value);
			}
		}
	}
}
