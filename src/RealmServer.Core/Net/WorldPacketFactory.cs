using System;
using System.IO;

namespace Hazzik.Net
{
    public class WorldPacketFactory
    {
        public static IPacket Create(WMSG code)
        {
            return new WorldPacket(code);
        }

        public static IPacketBuilder Build(WMSG wmsg, Action<BinaryWriter> builder)
        {
            return new LambdaPacketBuilder(wmsg, builder);
        }

        #region Nested type: LambdaPacketBuilder

        private class LambdaPacketBuilder : IPacketBuilder
        {
            private readonly Action<BinaryWriter> builder;
            private readonly WMSG wmsg;

            public LambdaPacketBuilder(WMSG wmsg, Action<BinaryWriter> builder)
            {
                this.wmsg = wmsg;
                this.builder = builder;
            }

            #region IPacketBuilder Members

            public bool IsEmpty
            {
                get { return false; }
            }

            public IPacket Build()
            {
                IPacket packet = Create(wmsg);
                builder(packet.CreateWriter());
                return packet;
            }

            #endregion
        }

        #endregion
    }
}