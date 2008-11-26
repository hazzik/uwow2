using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using ICSharpCode.SharpZipLib.Zip.Compression.Streams;

namespace Hazzik.Net {
	public class CompressedDataPacket : WorldPacket, ICollection<IPacket> {
		private readonly IList<IPacket> _packets = new List<IPacket>();
		private bool _dirty;

		public CompressedDataPacket()
			: base(WMSG.SMSG_COMPRESSED_DATA) {
		}

		public override Stream GetStream() {
			if(null == _stream || _dirty) {
				var size = 0;
				foreach(var packet in _packets) {
					size += 4 + packet.Size;
				}
				_stream = new MemoryStream();
				_stream.WriteByte((byte)(size));
				_stream.WriteByte((byte)(size >> 0x08));
				_stream.WriteByte((byte)(size >> 0x10));
				_stream.WriteByte((byte)(size >> 0x18));

				var compressedStream = new DeflaterOutputStream(_stream);
				foreach(var packet in _packets) {
					compressedStream.WriteByte((byte)(packet.Size + 2));
					compressedStream.WriteByte((byte)(Code));
					compressedStream.WriteByte((byte)(Code >> 0x08));
					packet.WriteBody(compressedStream);
				}
			}
			return _stream;
		}

		#region ICollection<IPacket> Members

		public void Add(IPacket item) {
			_packets.Add(item);
			_dirty = true;
		}

		public void Clear() {
			_packets.Clear();
			_dirty = true;
		}

		public bool Contains(IPacket item) {
			return _packets.Contains(item);
		}

		public void CopyTo(IPacket[] array, int arrayIndex) {
			_packets.CopyTo(array, arrayIndex);
		}

		public int Count {
			get { return _packets.Count; }
		}

		public bool IsReadOnly {
			get { return false; }
		}

		public bool Remove(IPacket item) {
			_dirty = true;
			return _packets.Remove(item);
		}

		#endregion

		#region IEnumerable<IPacket> Members

		public IEnumerator<IPacket> GetEnumerator() {
			return _packets.GetEnumerator();
		}

		#endregion

		#region IEnumerable Members

		IEnumerator IEnumerable.GetEnumerator() {
			return _packets.GetEnumerator();
		}

		#endregion
	}
}