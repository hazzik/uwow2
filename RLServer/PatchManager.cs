using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;

namespace UWoW {
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
