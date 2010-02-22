using System;
using System.IO;
using Hazzik.Chat;
using Hazzik.IO;
using Hazzik.Net;

namespace Hazzik.RealmServer.PacketDispatchers.Chat
{
    public static class PacketReader
    {
        public static T Read<T>(this IPacket packet, Func<BinaryReader, T> read)
        {
            return read(packet.CreateReader());
        }
    }

    [WorldPacketHandler(WMSG.CMSG_JOIN_CHANNEL)]
    public class JoinChannel : IPacketDispatcher
    {
        #region IPacketDispatcher Members

        public void Dispatch(ISession session, IPacket packet)
        {
            JoinChannelMsg msg = packet.Read(reader => new JoinChannelMsg
                                                                      {
                                                                          ChannelId = reader.ReadUInt32(),
                                                                          Unk1 = reader.ReadByte(),
                                                                          Unk2 = reader.ReadByte(),
                                                                          ChanneName = reader.ReadCString(),
                                                                          Password = reader.ReadCString()
                                                                      });

            var pkt = WorldPacketFactory.Create(WMSG.SMSG_CHANNEL_NOTIFY);
            var writer = pkt.CreateWriter();
            writer.Write((byte)ChannelNotification.YouJoined);
            writer.Write(msg.ChanneName);
            writer.Write((byte)0);
            writer.Write(msg.ChannelId);
            writer.Write(0);
            session.Send(pkt);
            Console.WriteLine("{0} {1} {2} {3} {4}", msg.ChannelId, msg.Unk1, msg.Unk2, msg.ChanneName, msg.Password);
        }

        #endregion
    }

    public class JoinChannelMsg
    {
        public uint ChannelId { get;  set; }

        public byte Unk1 { get;  set; }

        public byte Unk2 { get;  set; }

        public string ChanneName { get;  set; }

        public string Password { get;  set; }
    }
}