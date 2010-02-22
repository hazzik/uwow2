using System;
using System.IO;
using System.Text;
using Hazzik.Chat;
using Hazzik.IO;
using Hazzik.Map;
using Hazzik.Net;

namespace Hazzik.RealmServer.PacketDispatchers.Chat
{
    [WorldPacketHandler(WMSG.CMSG_MESSAGECHAT)]
    internal class MessageChat : IPacketDispatcher
    {
        #region IPacketDispatcher Members

        public void Dispatch(ISession session, IPacket packet)
        {
            BinaryReader reader = packet.CreateReader();
            var type = (MessageType) reader.ReadUInt32();
            uint language = reader.ReadUInt32();
            string channel = type == MessageType.Channel || type == MessageType.Whisper
                                 ? reader.ReadCString()
                                 : string.Empty;
            string message = reader.ReadCString();

            var pkt = WorldPacketFactory.Create(WMSG.SMSG_MESSAGECHAT);
            var writer = pkt.CreateWriter();
            writer.Write((byte)type);
            writer.Write((uint)0);
            writer.Write(session.Player.Guid);
            writer.Write(0);
/*
            if (target != null)
                writer.WritePascalString(target);
*/
            writer.Write((ulong)0);
            writer.WritePascalString(message);
            writer.Write((byte)0);

            var players = ObjectManager.GetPlayersNear(session.Player);
            foreach (var player in players)
            {
                player.Session.Send(pkt);
            }


            Console.WriteLine("{0} {1} {2} {3}", type, language, channel, message);
        }

        #endregion
    }
}