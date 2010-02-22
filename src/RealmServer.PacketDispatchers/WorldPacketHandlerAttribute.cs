using System;
using Hazzik.Annotations;
using Hazzik.Net;

namespace Hazzik.RealmServer.PacketDispatchers
{
    [MeansImplicitUse]
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    [BaseTypeRequired(typeof (IPacketDispatcher))]
    internal class WorldPacketHandlerAttribute : Attribute
    {
        private readonly WMSG messageType;

        public WorldPacketHandlerAttribute(WMSG messageType)
        {
            this.messageType = messageType;
        }

        public WMSG MessageType
        {
            get { return messageType; }
        }
    }
}