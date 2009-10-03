using System;
using System.IO;
using System.Net.Sockets;
using System.Security.Cryptography;

namespace Hazzik.Net {
	public class WorldClient : IWorldClient {
		protected ICryptoTransform decryptor;
		protected ICryptoTransform encryptor;

		protected bool firstPacket = true;
		protected Socket socket;

		public WorldClient(Socket socket) {
			this.socket = socket;
		}

		#region IWorldClient Members

		public void SetSymmetricAlgorithm(SymmetricAlgorithm symmetricAlgorithm) {
			decryptor = symmetricAlgorithm.CreateDecryptor();
			encryptor = symmetricAlgorithm.CreateEncryptor();
			firstPacket = false;
		}

		#endregion

		public virtual Stream GetStream() {
			return new NetworkStream(socket, false);
		}
	}
}