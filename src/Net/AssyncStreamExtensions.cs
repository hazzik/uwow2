using System;
using System.IO;

namespace Hazzik.Net {
	public static class AssyncStreamExtensions {
		public static void ReadAsync(this Stream stream, byte[] buffer, int offset, int count, Action callback) {
			BeginRead(new ReadState(stream, buffer) { Offset = offset, Count = count }, callback);
		}

		private static void BeginRead(ReadState rs, Action callback) {
			AsyncCallback asyncCallback = ar => {
			                              	int count = rs.Stream.EndRead(ar);
			                              	rs.Offset += count;
			                              	rs.Count -= count;
			                              	if(rs.Count > 0) {
			                              		BeginRead(rs, callback);
			                              	}
			                              	else {
			                              		callback();
			                              	}
			                              };
			rs.Stream.BeginRead(rs.Buffer, rs.Offset, rs.Count, asyncCallback, rs);
		}

		#region Nested type: ReadState

		private class ReadState {
			private readonly byte[] buffer;
			private readonly Stream stream;

			public ReadState(Stream stream, byte[] buffer) {
				this.stream = stream;
				this.buffer = buffer;
			}

			public Stream Stream {
				get { return stream; }
			}

			public byte[] Buffer {
				get { return buffer; }
			}

			public int Offset { get; set; }
			public int Count { get; set; }
		}

		#endregion
	}
}