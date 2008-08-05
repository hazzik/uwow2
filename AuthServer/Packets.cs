using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Collections;
using ICSharpCode.SharpZipLib.Zip.Compression.Streams;

namespace Hazzik.Net {
	public class ServerPacket : BinaryWriter, IPacket {
		public int Code { get; set; }
		public int Size { get; private set; }

		public ServerPacket(OpCodes o)
			: base(new MemoryStream()) {
			Code = (int)o;

			base.Seek(2, SeekOrigin.Begin);
			base.Write((ushort)Code);
		}

		public void WriteAt(int o, int position) {
			int p = (int)((MemoryStream)base.BaseStream).Position;
			base.Seek(position, SeekOrigin.Begin);
			Write(o);
			base.Seek(p, SeekOrigin.Begin);
		}

		public void WriteAt(byte o, int position) {
			int p = (int)((MemoryStream)base.BaseStream).Position;
			base.Seek(position, SeekOrigin.Begin);
			Write(o);
			base.Seek(p, SeekOrigin.Begin);
		}

		public override void Write(string a) {
			if(a == null)
				a = string.Empty;
			foreach(char c in a)
				Write((byte)c);
			Write((byte)0);
		}

		public virtual void Write(BitArray ba, int len) {
			int len1 = ba.Length / 8 + 1;
			if(len1 < len)
				len1 = len;

			byte[] tmp = new byte[len1];
			ba.CopyTo((Array)tmp, 0);
			Write(tmp, 0, len);
		}

		public void ForgePacket(byte val, int length) {
			for(int i = 0; i < length; i++)
				Write(val);
		}

		public byte[] GetComplete() {
			if(Code == (int)OpCodes.SMSG_UPDATE_OBJECT) {
				MemoryStream m_ms = (MemoryStream)base.BaseStream;
				int size = (int)m_ms.Length;
				if(size > 50) {
					Code = (int)OpCodes.SMSG_COMPRESSED_UPDATE_OBJECT;
					using(var w = new BinaryWriter(new MemoryStream())) {
						w.Write((ushort)0);
						w.Write((ushort)Code);
						w.Write(size);
						using(DeflaterOutputStream d = new DeflaterOutputStream(w.BaseStream)) {
							d.Write(m_ms.ToArray(), 4, (int)m_ms.Length - 4);
						}
						base.OutStream = new MemoryStream(((MemoryStream)w.BaseStream).ToArray());
					}
				}
			}

			long len = ((MemoryStream)base.BaseStream).Length;

			base.Seek(0, SeekOrigin.Begin);
			base.Write((byte)((len - 2) >> 8));
			base.Write((byte)((len - 2)));

			return ((MemoryStream)base.BaseStream).ToArray();
		}

		public Stream GetStream() {
			throw new NotImplementedException();
		}
	}

	public class ClientPacket : BinaryReader, IPacket {

		public int Code { get; set; }

		public int Size { get; private set; }

		public ClientPacket(byte[] data)
			: base(new MemoryStream(data)) {
			Size = (int)((data[0] << 8) | data[1]) + 2;
			Code = (int)(data[2] | (data[3] << 8));
			((MemoryStream)base.BaseStream).Seek(6, SeekOrigin.Begin);
		}

		public Stream GetStream() {
			throw new NotImplementedException();
		}

	}
}
