using System;
using System.IO;
using Hazzik.Map;
using Hazzik.Net;

namespace Hazzik.Objects {
	public class Session : ISession {
		private readonly IWorldClient _sender;
		private Player _player;

		public Session(IWorldClient sender) {
			_sender = sender;
		}

		#region ICommunicationService Members

		public Player Player {
			get { return _player; }
			set {
				if(null == value) {
					_player.Session = null;
					_player = value;
				}
				else {
					_player = value;
					_player.Session = this;
				}
			}
		}

		public Account Account { get; set; }

		public IWorldClient Client {
			get { return _sender; }
		}

		public void SendHeartBeat() {
			IPacket packet = WorldPacketFactory.Create(WMSG.MSG_MOVE_HEARTBEAT);
			BinaryWriter writer = packet.CreateWriter();
			writer.WritePackGuid(Player.Guid);
			Player.MovementInfo.Write(writer);
			SendNear(Player, packet);
		}

		public void SendInitialSpells() {
			Client.Send(GetInitialSpellsPkt());
		}

		#endregion

		public static void SendNear(Positioned me, IPacket responce) {
			foreach(Player player in ObjectManager.GetPlayersNear(me)) {
				if(player.Session != null) {
					player.Session.Client.Send(responce);
				}
			}
		}

		public static void SendNearExceptMe(Positioned me, IPacket responce) {
			foreach(Player player in ObjectManager.GetPlayersNear(me)) {
				if(player != me && player.Session != null) {
					player.Session.Client.Send(responce);
				}
			}
		}

		private IPacket GetInitialSpellsPkt() {
			IPacket packet = WorldPacketFactory.Create(WMSG.SMSG_INITIAL_SPELLS);
			BinaryWriter writer = packet.CreateWriter();
			writer.Write((byte)0);
			writer.Write((ushort)Player.Spells.Count);
			foreach(int i in Player.Spells) {
				writer.Write(i);
			}
			writer.Write((ushort)0);
			return packet;
		}
	}
}