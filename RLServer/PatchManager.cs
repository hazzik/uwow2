using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;

namespace UWoW {
	[Serializable]
	public class VersionInfo {
		private string _clientTag;
		[System.Xml.Serialization.XmlAttribute]
		public string ClientTag {
			get { return _clientTag; }
			set { _clientTag = value; }
		}

		private Version _version;
		[System.Xml.Serialization.XmlIgnore]
		public Version Version {
			get { return _version; }
			set { _version = value; }
		}

		[System.Xml.Serialization.XmlAttribute(AttributeName = "Version")]
		public string VersionTag {
			get { return _version.ToString(4); }
			set { _version = new Version(value); }
		}

		private string _platform;
		[System.Xml.Serialization.XmlAttribute]
		public string Platform {
			get { return _platform; }
			set { _platform = value; }
		}

		private string _os;
		[System.Xml.Serialization.XmlAttribute]
		public string OS {
			get { return _os; }
			set { _os = value; }
		}

		private string _locale;
		[System.Xml.Serialization.XmlAttribute]
		public string Locale {
			get { return _locale; }
			set { _locale = value; }
		}
	}

	[Serializable]
	public class PatchInfo {
		VersionInfo _versionInfo = new VersionInfo();
		FileInfo _fileInfo;

		public VersionInfo VersionInfo {
			get { return _versionInfo; }
			set { _versionInfo = value; }
		}

		public string FileName {
			get { return _fileInfo != null ? _fileInfo.Name : string.Empty; }
			set { _fileInfo = new FileInfo(value); }
		}

		byte[] _md5;
		public byte[] MD5 {
			get {
				if(_md5 == null) {
					CalculateMD5();
				}
				return _md5;
			}
			set { _md5 = value; }
		}

		private void CalculateMD5() {
			var md5 = System.Security.Cryptography.MD5.Create();
			_md5 = md5.ComputeHash(_fileInfo.OpenRead());
		}
	}
}
