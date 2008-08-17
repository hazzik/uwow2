using System;
using System.Net.Sockets;
using System.IO;
using System.Collections.Generic;

namespace Hazzik.Net {
	public abstract class ClientBase {
		protected Socket _socket;
		private Stream _stream;
		private Queue<IPacket> _packets = new Queue<IPacket>();

		public ClientBase(Socket socket) {
			_socket = socket;
		}

		public abstract IPacket ReadPacket();
		public abstract void WritePacket(IPacket packet);
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
				  