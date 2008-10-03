﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.IO;
using System.Collections;

namespace Hazzik {
	public class AddonManager {
		static XmlSerializer _serializer = new XmlSerializer(typeof(List<AddonInfo>));
		static AddonManager _instance;
		public static AddonManager Instance {
			get {
				if(_instance == null) {
					_instance = new AddonManager();
				}
				return _instance;
			}
		}

		public List<AddonInfo> AddonInfos = new List<AddonInfo>();

		public void Load(string fileName) {
			using(var file = new FileInfo(fileName).Open(FileMode.OpenOrCreate, FileAccess.Read)) {
				AddonInfos = (List<AddonInfo>)_serializer.Deserialize(file);
			}
		}

		public void Save(string filename) {
			using(var file = new FileInfo(filename).Open(FileMode.Create, FileAccess.Write)) {
				_serializer.Serialize(file, AddonInfos);
			}
		}

		public AddonInfo this[string name] {
			get {
				return (from addon in AddonInfos
						  where addon.Name == name
						  select addon).FirstOrDefault();
			}
			set {
				var addon = this[name];
				if(addon != null) {
					AddonInfos.Remove(addon);
				}
				AddonInfos.Add(value);
			}
		}
	}
}
