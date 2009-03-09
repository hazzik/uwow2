using System;
using System.IO;

namespace Hazzik.Net {
	public static class AssyncStreamExtensions {
		public static void ReadAsync(this Stream stream, byte[] buffer, int offset, int count, Action func) {
			BeginRead(new ReadState(stream, buffer) { Offset = offset, Count = count }, func);
		}

		private static void BeginRead(ReadState rs, Action func) {
			AsyncCallback callback = (ar => {
			                          	int count = rs.Stream.EndRead(ar);
			                          	rs.Offset += count;
			                          	rs.Count -= count;
			                          	if(rs.Count > 0) {
			                          		BeginRead(rs, func);
			                          	}
			                          	else {
			                          		func();
			                          	}
			                          });
			rs.Stream.BeginRead(rs.Buffer, rs.Offset, rs.Count, callback, rs);
		}

		#region Nested type: ReadState

		private class ReadState {
			private readonly byte[] _buffer;
			private readonly Stream _stream;

			public ReadState(Stream stream, byte[] buffer) {
				_stream = stream;
				_buffer = buffer;
			}

			public Stream Stream {
				get { return _stream; }
			}

			public byte[] Buffer {
				get { return _buffer; }
			}

			public int Offset { get; set; }
			public int Count { get; set; }
		}

		#endregion
	}
}