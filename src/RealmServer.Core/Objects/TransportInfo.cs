using System;
using System.IO;

namespace Hazzik.Objects {
	public class TransportInfo {
		public ulong Guid { get; set; }

		public float X { get; set; }

		public float Y { get; set; }

		public float Z { get; set; }

		public float O { get; set; }

		public uint Time { get; set; }

		public byte Seat { get; set; }

		public void Write(BinaryWriter w) {
			w.WritePackGuid(Guid);
			w.Write(X);
			w.Write(Y);
			w.Write(Z);
			w.Write(O);
			w.Write(Time);
			w.Write(Seat);
		}

		public void Read(BinaryReader r) {
			Guid = r.ReadPackGuid();
			X = r.ReadSingle();
			Y = r.ReadSingle();
			Z = r.ReadSingle();
			O = r.ReadSingle();
			Time = r.ReadUInt32();
			Seat = r.ReadByte();
		}
	}
}