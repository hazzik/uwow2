using System;
using System.IO;
using System.Security.Cryptography;

namespace Hazzik {
	[Serializable]
	public class PatchInfo {
		private static HashAlgorithm s_hashAlgo = MD5.Create();

		private VersionInfo _versionInfo = new VersionInfo();
		private FileInfo _fileInfo;
		private byte[] _hash;

		public VersionInfo VersionInfo {
			get { return _versionInfo; }
			set { _versionInfo = value; }
		}

		public string FileName {
			get { return _fileInfo != null ? _fileInfo.Name : string.Empty; }
			set { _fileInfo = new FileInfo(value); }
		}

		public long Length {
			get { return _fileInfo != null ? _fileInfo.Length : 0; }
		}

		public byte[] Hash {
			get {
				if(_hash == null) {
					_hash = s_hashAlgo.ComputeHash(_fileInfo.OpenRead());
				}
				return _hash;
			}
			set { _hash = value; }
		}

		public Stream OpenRead() {
			return _fileInfo.OpenRead();
		}
	}
}