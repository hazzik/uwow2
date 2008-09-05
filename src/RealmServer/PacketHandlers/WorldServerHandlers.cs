using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hazzik.Net;
using Hazzik.Helper;
using Hazzik.Attributes;
using System.IO;
using Hazzik.Objects;

namespace Hazzik {
	[PacketHandlerClass]
	public static class WorldServerHandlers {
		[WorldPacketHandler(WMSG.CMSG_REALM_SPLIT)]
		public static void HandleCMSG_REALM_SPLIT(ClientBase client, IPacket packet) {
			var r = packet.GetReader();
			var unk1 = r.ReadUInt32();

			var responce = new WorldPacket(WMSG.SMSG_REALM_SPLIT);
			var w = responce.GetWriter();
			w.Write(unk1);
			//0-normal, 1-split, 2-split pending;
			w.Write(0);
			w.WriteCString(DateTime.Now.AddDays(1).ToShortDateString());
			w.WriteCString("hello");
			w.WriteCString("world");
			client.SendPacket(responce);
		}

		[WorldPacketHandler(WMSG.CMSG_CHAR_ENUM)]
		public static void HandleCMSG_CHAR_ENUM(ClientBase client, IPacket packet) {
			try {
				var wclient = client as WorldClient;
				var p = new WorldPacket(WMSG.SMSG_CHAR_ENUM);
				var w = p.GetWriter();
				w.Write((byte)wclient.Account.Players.Count());
				foreach(var player in wclient.Account.Players) {
					player.WriteSMSG_CHAR_ENUM(w);
				}
				client.SendPacket(p);
			} catch(Exception e) {
				Console.WriteLine(e.Message);
			}
		}

		[WorldPacketHandler(WMSG.CMSG_CHAR_CREATE)]
		public static void HandleCMSG_CHAR_CREATE(ClientBase client, IPacket packet) {
			var r = packet.GetReader();
			var player = new Player() {
				Name = r.ReadCString(),
				Race = (Races)r.ReadByte(),
				Classe = (Classes)r.ReadByte(),
				Gender = r.ReadByte(),
				skin = r.ReadByte(),
				face = r.ReadByte(),
				hairStyle = r.ReadByte(),
				hairColor = r.ReadByte(),
				facialHair = r.ReadByte(),
			};
			(client as WorldClient).Account.AddPlayer(player);
			var responce = new WorldPacket(WMSG.SMSG_CHAR_CREATE);
			var w = responce.GetWriter();
			w.Write((byte)47);
			client.SendPacket(responce);
		}

		[WorldPacketHandler(WMSG.CMSG_PLAYER_LOGIN)]
		public static void HandleCMSG_PLAYER_LOGIN(ClientBase client, IPacket packet) {
			var reader = packet.GetReader();
			var guid = reader.ReadInt64();
			var player = (from players in (client as WorldClient).Account.Players
							  where players.Guid == guid
							  select players).FirstOrDefault();
			var r = (WorldPacket)null;
			var w = (BinaryWriter)null;
			if(null == player) {
				r = new WorldPacket(WMSG.SMSG_CHARACTER_LOGIN_FAILED);
				w = r.GetWriter();
				w.Write((byte)0x44);
				client.SendPacket(r);
			}
			else {
				r = new WorldPacket(WMSG.SMSG_LOGIN_SETTIMESPEED);
				w = r.GetWriter();
				w.Write(Program.GetActualTime());
				w.Write(0.01666667F);
				client.SendPacket(r);

				r = new WorldPacket(WMSG.SMSG_UPDATE_OBJECT);
				w = r.GetWriter();
				w.Write(1);
				w.Write((byte)0);
				w.Write((byte)3);
				w.Write((byte)4);
				w.Write((byte)(1 | 0x20 | 0x40));
				w.Write(0);
				w.Write((byte)0);
				w.Write(0);
				w.Write(0);
				w.Write(0);
				w.Write(0);
				w.Write(0);
				w.Write(0);
				w.Write(0);
				w.Write(0);
				w.Write(0);
				w.Write(0);
				w.Write(0);
				w.Write(0);
				w.Write(0);
				w.Write(0);
				w.Write((byte)0);
				client.SendPacket(r);
			}
		}
	}
}
