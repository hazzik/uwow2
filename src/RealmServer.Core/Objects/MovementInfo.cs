using System;
using System.IO;

namespace Hazzik.Objects {
	public class MovementInfo {
		public MovementFlags Flags { get; set; }

		public ushort Unk1 { get; set; }

		public uint Time { get; set; }

		public float X { get; set; }

		public float Y { get; set; }

		public float Z { get; set; }

		public float O { get; set; }

		public ulong TransportGuid { get; set; }

		public float TransportX { get; set; }

		public float TransportY { get; set; }

		public float TransportZ { get; set; }

		public float TransportO { get; set; }

		public uint TransportTime { get; set; }

		public byte TransportUnk1 { get; set; }

		public float SwimmPitch { get; set; }

		public uint FallTime { get; set; }

		public float JumpUnk1 { get; set; }

		public float JumpSin { get; set; }

		public float JumpCos { get; set; }

		public float JumpSpeed { get; set; }

		public float Spline { get; set; }

		public void Write(BinaryWriter w) {
			lock(this) {
				w.Write((uint)Flags);
				w.Write(Unk1);
				w.Write(Time);
				w.Write(X);
				w.Write(Y);
				w.Write(Z);
				w.Write(O);
				if((Flags & MovementFlags.OnTransport) != 0) {
					w.WritePackGuid(TransportGuid);
					w.Write(TransportX);
					w.Write(TransportY);
					w.Write(TransportZ);
					w.Write(TransportO);
					w.Write(TransportTime);
					w.Write(TransportUnk1);
				}
				if((Flags & (MovementFlags.Swimming | MovementFlags.Unk3)) != 0) {
					w.Write(SwimmPitch);
				}
				w.Write(FallTime);
				if((Flags & MovementFlags.Jumping) != 0) {
					w.Write(JumpUnk1);
					w.Write(JumpSin);
					w.Write(JumpCos);
					w.Write(JumpSpeed);
				}
				if((Flags & MovementFlags.Spline) != 0) {
					w.Write(Spline);
				}
			}
		}

		public void Read(BinaryReader r) {
			lock(this) {
				Flags = ((MovementFlags)r.ReadUInt32());
				Unk1 = r.ReadUInt16();
				Time = r.ReadUInt32();
				X = r.ReadSingle();
				Y = r.ReadSingle();
				Z = r.ReadSingle();
				O = r.ReadSingle();
				if((Flags & MovementFlags.OnTransport) != 0) {
					TransportGuid = r.ReadUInt64();
					TransportX = r.ReadSingle();
					TransportY = r.ReadSingle();
					TransportZ = r.ReadSingle();
					TransportO = r.ReadSingle();
					TransportTime = r.ReadUInt32();
					TransportUnk1 = r.ReadByte();
				}
				if((Flags & (MovementFlags.Swimming| MovementFlags.Unk3)) != 0) {
					SwimmPitch = r.ReadSingle();
				}
				FallTime = r.ReadUInt32();
				if((Flags & MovementFlags.Jumping) != 0) {
					JumpUnk1 = r.ReadSingle();
					JumpSin = r.ReadSingle();
					JumpCos = r.ReadSingle();
					JumpSpeed = r.ReadSingle();
				}
				if((Flags & MovementFlags.Spline) != 0) {
					Spline = r.ReadSingle();
				}
			}
		}
	}
}