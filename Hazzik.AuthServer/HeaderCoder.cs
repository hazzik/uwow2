using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UWoW {
	public class HeaderCoder {
		private byte[] SS = new byte[40];
		private int _d_pointer;
		private int _e_pointer;
		private byte _d_last_ch;
		private byte _e_last_ch;

		public HeaderCoder(byte[] ss) {
			SS = (byte[])ss.Clone();
		}

		public void Decode(byte[] buffer, int offset, int length) {
			for(int i = offset; i < offset + length; i++) {
				_d_pointer = _d_pointer % 40;
				byte x = (byte)((buffer[i] - _d_last_ch) ^ SS[_d_pointer++]);
				_d_last_ch = buffer[i];
				buffer[i] = x;
			}
		}

		public void Encode(byte[] buffer, int offset, int length) {
			for(int i = offset; i < offset + length; i++) {
				buffer[i] = (byte)(_e_last_ch + (SS[_e_pointer++] ^ buffer[i]));
				_e_pointer = _e_pointer % 40;
				_e_last_ch = buffer[i];
			}
		}
	}
}
