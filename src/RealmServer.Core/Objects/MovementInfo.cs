using System;
using System.IO;

namespace Hazzik.Objects {
    public class MovementInfo {
        public MovementFlags Flags { get; set; }

        public MovementFlags2 Flags2 { get; set; }

        public uint Time { get; set; }

        public float X { get; set; }

        public float Y { get; set; }

        public float Z { get; set; }

        public float O { get; set; }

        public float SwimmPitch { get; set; }

        public uint FallTime { get; set; }

        public float JumpUnk1 { get; set; }

        public float JumpSin { get; set; }

        public float JumpCos { get; set; }

        public float JumpSpeed { get; set; }

        public float SplineElevation { get; set; }

        public TransportInfo Transport { get; set; }

        public void Write(BinaryWriter w) {
            lock(this) {
                w.Write((uint)Flags);
                w.Write((ushort)Flags2);
                w.Write(Time);
                w.Write(X);
                w.Write(Y);
                w.Write(Z);
                w.Write(O);
                if(Flags.HasFlag(MovementFlags.OnTransport)) {
                    Transport.Write(w);
                }
                if(Flags.HasFlag(MovementFlags.Swimming | MovementFlags.Flying) ||
                   Flags2.HasFlag(MovementFlags2.AlwaysAllowPitching)) {
                    w.Write(SwimmPitch);
                }
                w.Write(FallTime);
                if(Flags.HasFlag(MovementFlags.Falling)) {
                    w.Write(JumpUnk1);
                    w.Write(JumpSin);
                    w.Write(JumpCos);
                    w.Write(JumpSpeed);
                }
                if(Flags.HasFlag(MovementFlags.SplineElevation)) {
                    w.Write(SplineElevation);
                }
            }
        }

        public void Read(BinaryReader r) {
            lock(this) {
                Flags = ((MovementFlags)r.ReadUInt32());
                Flags2 = (MovementFlags2)r.ReadUInt16();
                Time = r.ReadUInt32();
                X = r.ReadSingle();
                Y = r.ReadSingle();
                Z = r.ReadSingle();
                O = r.ReadSingle();
                if(Flags.HasFlag(MovementFlags.OnTransport)) {
                    if(Transport == null) {
                        Transport = new TransportInfo();
                    }
                    Transport.Read(r);
                }
                else {
                    Transport = null;
                }
                if(Flags.HasFlag(MovementFlags.Swimming | MovementFlags.Flying) ||
                   Flags2.HasFlag(MovementFlags2.AlwaysAllowPitching)) {
                    SwimmPitch = r.ReadSingle();
                }
                FallTime = r.ReadUInt32();
                if(Flags.HasFlag(MovementFlags.Falling)) {
                    JumpUnk1 = r.ReadSingle();
                    JumpSin = r.ReadSingle();
                    JumpCos = r.ReadSingle();
                    JumpSpeed = r.ReadSingle();
                }
                if(Flags.HasFlag(MovementFlags.SplineElevation)) {
                    SplineElevation = r.ReadSingle();
                }
            }
        }
    }
}