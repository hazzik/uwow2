using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using ICSharpCode.SharpZipLib.Zip.Compression.Streams;

namespace Hazzik.Net {
	internal class CompressedDataPacket : WorldPacket, ICollection<IPacket> {
		private readonly IList<IPacket> packets = new List<IPacket>();
		private bool dirty;

		public CompressedDataPacket()
			: base(WMSG.SMSG_COMPRESSED_MOVES) {
		}

		#region ICollection<IPacket> Members

		public void Add(IPacket item) {
			packets.Add(item);
			dirty = true;
		}

		public void Clear() {
			packets.Clear();
			dirty = true;
		}

		public bool Contains(IPacket item) {
			return packets.Contains(item);
		}

		public void CopyTo(IPacket[] array, int arrayIndex) {
			packets.CopyTo(array, arrayIndex);
		}

		public int Count {
			get { return packets.Count; }
		}

		public bool IsReadOnly {
			get { return false; }
		}

		public bool Remove(IPacket item) {
			dirty = true;
			return packets.Remove(item);
		}

		IEnumerator IEnumerable.GetEnumerator() {
			return packets.GetEnumerator();
		}

		IEnumerator<IPacket> IEnumerable<IPacket>.GetEnumerator() {
			return packets.GetEnumerator();
		}

		#endregion

		public override Stream GetStream() {
			if(null == _stream || dirty) {
				int size = 0;
				foreach(IPacket packet in packets) {
					size += 3 + packet.Size;
				}
				_stream = new MemoryStream();
				_stream.WriteByte((byte)(size));
				_stream.WriteByte((byte)(size >> 0x08));
				_stream.WriteByte((byte)(size >> 0x10));
				_stream.WriteByte((byte)(size >> 0x18));

				var compressedStream = new DeflaterOutputStream(_stream);
				foreach(IPacket packet in packets) {
					compressedStream.WriteByte((byte)(packet.Size + 2));
					compressedStream.WriteByte((byte)(Code));
					compressedStream.WriteByte((byte)(Code >> 0x08));
					packet.WriteBody(compressedStream);
				}
			}
			return _stream;
		}
	}
}