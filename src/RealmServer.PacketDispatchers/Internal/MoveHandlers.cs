using System;
using System.IO;
using Hazzik.Net;
using Hazzik.Objects;

namespace Hazzik.RealmServer.PacketDispatchers.Internal {
	[WorldPacketHandler(WMSG.MSG_MOVE_START_FORWARD)]
	[WorldPacketHandler(WMSG.MSG_MOVE_START_BACKWARD)]
	[WorldPacketHandler(WMSG.MSG_MOVE_STOP)]
	[WorldPacketHandler(WMSG.MSG_MOVE_START_STRAFE_LEFT)]
	[WorldPacketHandler(WMSG.MSG_MOVE_START_STRAFE_RIGHT)]
	[WorldPacketHandler(WMSG.MSG_MOVE_STOP_STRAFE)]
	[WorldPacketHandler(WMSG.MSG_MOVE_JUMP)]
	[WorldPacketHandler(WMSG.MSG_MOVE_START_TURN_LEFT)]
	[WorldPacketHandler(WMSG.MSG_MOVE_START_TURN_RIGHT)]
	[WorldPacketHandler(WMSG.MSG_MOVE_STOP_TURN)]
	[WorldPacketHandler(WMSG.MSG_MOVE_START_PITCH_UP)]
	[WorldPacketHandler(WMSG.MSG_MOVE_START_PITCH_DOWN)]
	[WorldPacketHandler(WMSG.MSG_MOVE_STOP_PITCH)]
	[WorldPacketHandler(WMSG.MSG_MOVE_SET_RUN_MODE)]
	[WorldPacketHandler(WMSG.MSG_MOVE_SET_WALK_MODE)]
	[WorldPacketHandler(WMSG.MSG_MOVE_START_SWIM)]
	[WorldPacketHandler(WMSG.MSG_MOVE_STOP_SWIM)]
	[WorldPacketHandler(WMSG.MSG_MOVE_SET_FACING)]
	[WorldPacketHandler(WMSG.MSG_MOVE_SET_PITCH)]
	[WorldPacketHandler(WMSG.MSG_MOVE_ROOT)]
	[WorldPacketHandler(WMSG.MSG_MOVE_UNROOT)]
	[WorldPacketHandler(WMSG.MSG_MOVE_HEARTBEAT)]
	[WorldPacketHandler(WMSG.MSG_MOVE_KNOCK_BACK)]
	[WorldPacketHandler(WMSG.MSG_MOVE_HOVER)]
	[WorldPacketHandler(WMSG.MSG_MOVE_FALL_LAND)]
	internal class MoveHandlers : IPacketDispatcher {
		#region IPacketDispatcher Members

		public void Dispatch(ISession client, IPacket packet) {
			Player me = client.Player;
			Session.SendNearExceptMe(me, GetMoveResponce(packet));
			BinaryReader reader = packet.CreateReader();
			reader.BaseStream.Seek(0, SeekOrigin.Begin);
			ulong guid = reader.ReadPackGuid();
			if(guid != me.Guid) {
				throw new Exception();
			}
			me.MovementInfo.Read(reader);
		}

		#endregion

		private static IPacket GetMoveResponce(IPacket packet) {
			BinaryReader reader = packet.CreateReader();
			reader.BaseStream.Seek(0, SeekOrigin.Begin);
			byte[] bytes = reader.ReadBytes(packet.Size);

			IPacket responce = WorldPacketFactory.Create((WMSG)packet.Code);
			BinaryWriter w = responce.CreateWriter();
			w.Write(bytes);
			return responce;
		}
	}
}