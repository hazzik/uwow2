using System;
using System.Net.Sockets;
using System.IO;

namespace UWoW.Net {
	public abstract class ClientBase {
		protected int _headerSize;
		protected Socket _socket;

		public ClientBase(int headerSize, Socket socket) {
			_socket = socket;
			_headerSize = headerSize;
		}

		public abstract byte[] ReadPacket();
		public abstract int PacketCode(byte[] header);
		public abstract int PacketSize(byte[] header);
		public abstract void ProcessData(byte[] data, int offset, int length);

		public virtual void Start() {
			try {
				byte[] data;
				while(true) {
					data = ReadPacket();
					ProcessData(data, 0, data.Length);
				}
			} catch(SocketException) {
			} catch(Exception e) {
				Console.WriteLine(e.Message);
				Console.WriteLine(e.StackTrace);
			}
			_socket.Close();
		}
	}
}
				  