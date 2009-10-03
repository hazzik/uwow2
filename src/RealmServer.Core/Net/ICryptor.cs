using System;
using System.IO;
using System.Security.Cryptography;

namespace Hazzik.Net {
	public interface ICryptor {
		void SetSymmetricAlgorithm(SymmetricAlgorithm symmetricAlgorithm);
		Stream EncryptStream(Stream data);
		Stream DecryptStream(Stream data);
	}
}