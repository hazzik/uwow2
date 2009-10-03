using System;
using System.IO;
using System.Security.Cryptography;

namespace Hazzik.Net {
	internal class Cryptor : ICryptor {
		protected internal ICryptoTransform decryptor;
		protected internal ICryptoTransform encryptor;

		#region ICryptor Members

		public void SetSymmetricAlgorithm(SymmetricAlgorithm symmetricAlgorithm) {
			decryptor = symmetricAlgorithm.CreateDecryptor();
			encryptor = symmetricAlgorithm.CreateEncryptor();
		}

		public Stream EncryptStream(Stream data) {
			return encryptor == null ? data : new CryptoStream(data, encryptor, CryptoStreamMode.Write);
		}

		public Stream DecryptStream(Stream data) {
			return decryptor == null ? data : new CryptoStream(data, decryptor, CryptoStreamMode.Read);
		}

		#endregion
	}
}