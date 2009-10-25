using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using Hazzik.Data;
using Hazzik.IO;
using Hazzik.Net;
using Hazzik.Objects;

namespace Hazzik {
	public class AccpuntData {
		public ulong Guid { get; set; }
		public AccountDataType Type { get; set; }
		public DateTime Time { get; set; }
		public string Data { get; set; }
	}

	[XmlType("account")]
	public class Account {
		private IList<AccpuntData> datas = new List<AccpuntData>();
		private IList<Player> players = new List<Player>();

		public IList<Player> Players {
			get { return players; }
			protected set { players = value; }
		}

		public IList<AccpuntData> Datas {
			get { return datas; }
			protected set { datas = value; }
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

		public AccpuntData FindAccpuntData(AccountDataType type, ulong guid) {
			if(Datas.Where(ad => ad.Type == type && ad.Guid == guid).FirstOrDefault() != null) {
				return datas.Where(ad => ad.Type == type && ad.Guid == guid).FirstOrDefault();
			}
			var data = new AccpuntData {
				Type = type,
				Guid = guid,
				Time = DateTimeExtension.ToDateTime(0),
				Data = "",
			};
			datas.Add(data);
			return data;
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

		public void SetAccountData(AccpuntData accpuntData) {
			datas.Remove(datas.Where(ad => ad.Type == accpuntData.Type).FirstOrDefault());
			datas.Add(accpuntData);
		}
	}
}