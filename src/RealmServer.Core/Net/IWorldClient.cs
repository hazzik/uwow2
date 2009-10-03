using System;
using System.Security.Cryptography;

namespace Hazzik.Net {
	public interface IWorldClient {
		void SetSymmetricAlgorithm(SymmetricAlgorithm symmetricAlgorithm);
	}
}