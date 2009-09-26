using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using Hazzik.Net;
using Hazzik.Objects;

namespace Hazzik {
	[XmlType("account")]
	public class Account {
		private IList<Player> players = new List<Player>();

		public IList<Player> Players {
			get { return players; }
			private set { players = value; }
		}

		public virtual int Id { get; set; }
		public virtual string Name { get; set; }
		public virtual int Expansion { get; set; }
		public virtual byte[] PasswordSalt { get; set; }
		public virtual byte[] PasswordVerifier { get; set; }
		public virtual byte[] SessionKey { get; set; }

		public Player GetPlayer(ulong guid) {
			return (from player in players
			        where player.Guid == guid
			        select player).FirstOrDefault();
		}

		public void AddPlayer(Player player) {
			players.Add(player);
		}

		public void DelPlayer(Player player) {
			players.Remove(player);
		}

		#region packets

		public IPacket GetCharEnumPkt() {
			IPacket p = WorldPacketFactory.Create(WMSG.SMSG_CHAR_ENUM);
			BinaryWriter w = p.CreateWriter();
			w.Write((byte)Players.Count);
			foreach(Player player in Players) {
				w.Write(player.Guid);
				w.WriteCString(player.Name);
				w.Write((byte)player.Race);
				w.Write((byte)player.Classe);
				w.Write((byte)player.Gender);
				w.Write(player.Skin);
				w.Write(player.Face);
				w.Write(player.HairStyle);
				w.Write(player.HairColor);
				w.Write(player.FacialHair);
				w.Write((byte)player.Level);

				w.Write(player.ZoneId);
				w.Write(player.MapId);
				w.Write(player.PosX);
				w.Write(player.PosY);
				w.Write(player.PosZ);
				w.Write(player.GuildId);

				uint flag = 0x00000000;
				w.Write(flag);
				w.Write((byte)0);
				w.Write(player.PetDisplayId);
				w.Write(player.PetLevel);
				w.Write(player.PetCreatureFamily);
				w.Write(0);
				for(int i = 0; i < 20; i++) {
					Item item = player.Inventory[i];
					if(item != null) {
						w.Write(item.Template.DisplayId);
						w.Write((byte)item.Template.InventoryType);
						w.Write(0);
					}
					else {
						w.Write(0);
						w.Write((byte)0);
						w.Write(0);
					}
				}
			}
			return p;
		}

		#endregion
	}
}