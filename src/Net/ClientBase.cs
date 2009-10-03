using System;
using System.IO;
using System.Net.Sockets;

namespace Hazzik.Net {
	public abstract class ClientBase : IPacketSender, IClient {
		protected IPacketProcessor processor;
		protected Socket socket;

		protected ClientBase(Socket socket) {
			this.socket = socket;
		}

		#region IPacketSender Members

		public abstract void Send(IPacket packet);

		#endregion

		public abstract void ReadPacketAsync(Action<IPacket> func);
		
		public abstract IPacket ReadPacket();

		public virtual void Start() {
			try {
				while(true) {
					processor.Process(ReadPacket());
				}
			}
			catch(SocketException) {
			}
			catch(Exception e) {
				Console.WriteLine(e.Message);
				Console.WriteLine(e.StackTrace);
			}
			socket.Close();
		}

		public virtual Stream GetStream() {
			return new NetworkStream(socket, false);
		}
	}
}