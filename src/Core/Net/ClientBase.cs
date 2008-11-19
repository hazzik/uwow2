using System;
using System.IO;
using System.Net.Sockets;

namespace Hazzik.Net {
	public abstract class ClientBase : ISession {
		protected Socket _socket;
		private Stream _stream;

		public ClientBase(Socket socket) {
			_socket = socket;
		}

		public abstract IPacket ReadPacket();
		public abstract void Send(IPacket packet);
		public abstract void ProcessData(IPacket packet);

		public virtual void Start() {
			try {
				while(true) {
					IPacket data = ReadPacket();
					ProcessData(data);
				}
			}
			catch(SocketException) {
			}
			catch(Exception e) {
				Console.WriteLine(e.Message);
				Console.WriteLine(e.StackTrace);
			}
			_socket.Close();
		}

		public virtual Stream GetStream() {
			//if(_stream == null) {
			_stream = new NetworkStream(_socket, false);
			//}
			return _stream;
		}
	}
}