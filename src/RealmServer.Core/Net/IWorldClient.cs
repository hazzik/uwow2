using System;
using System.Security.Cryptography;

namespace Hazzik.Net {
	public interface IWorldClient : IPacketSender {
		void SetSymmetricAlgorithm(SymmetricAlgorithm cryptRc4);
	}
}