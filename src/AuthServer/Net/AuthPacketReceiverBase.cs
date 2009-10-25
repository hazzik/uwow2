using System;
using System.IO;
using System.Net.Sockets;

namespace Hazzik.Net {
	internal class AuthPacketReceiverBase : SocketHolder {
		protected AuthPacketReceiverBase(Socket client) : base(client) {
		}

		protected static int ReadCode(BinaryReader reader) {
			return reader.ReadByte();
		}

		protected static int ReadSize(BinaryReader reader, int code) {
			switch((RMSG)code) {
			case RMSG.AUTH_LOGON_CHALLENGE:
			case RMSG.AUTH_LOGON_RECODE_CHALLENGE:
				byte unk = reader.ReadByte();
				return reader.ReadUInt16();
			case RMSG.AUTH_LOGON_PROOF:
				return 0x4A;
			case RMSG.AUTH_LOGON_RECODE_PROOF:
				return 0x39;
			case RMSG.REALM_LIST:
				return 0x04;
			case RMSG.XFER_RESUME:
				return 0x08;
				//case RMSG.XFER_ACCEPT:
				//case RMSG.XFER_CANCEL:
				//default:
				//   return 0x00;
			}
			return 0x00;
		}
	}
}