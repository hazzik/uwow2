using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UWoW.Net;
using System.Net.Sockets;

namespace UWoW {
	public class WorldSereverClient : AClient {
		HeaderCoder _coder;
		bool _firstPacket = true;

		public WorldSereverClient(Socket socket)
			: base(6, socket) {

		}

		public override int PacketCode(byte[] header) {
			return BitConverter.ToInt16(header, 2);
		}

		public override int PacketSize(byte[] header) {
			if(!_firstPacket) {
				_coder.Decode(header, 0, 6);
			}
			return (int)((header[0] << 8) | header[1]) + 2;
		}

		public override void Start() {
			throw new NotImplementedException();
		}

		public override void PocessData(byte[] data, int offset, int length) {
			throw new NotImplementedException();
		}
	}
}
