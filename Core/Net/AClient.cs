using System;
using System.Net.Sockets;

namespace UWoW.Net
{
	public abstract class AClient : IDisposable
	{
		private bool disposed = false;
		//protected Socket clientSocket;
		protected byte[] receivedBuff;

		//public AClient(Socket s)
		//{
		//    clientSocket = s;
		//}

		public abstract void Start();
		public abstract void PocessData(byte[] data, int offset, int length);

		#region Destruction
		~AClient()
		{
			Dispose();
		}

		public void Dispose()
		{
			if (!disposed)
			{
				disposed = true;
			}
		}
		#endregion

	}
}
				  