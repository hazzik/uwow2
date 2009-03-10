using System;
using System.Collections.Generic;
using System.Linq;
using Hazzik.Net;
using Hazzik.Objects;

namespace Hazzik {
	[System.Xml.Serialization.XmlType("account")]
	public class Account {
		protected readonly IList<Player> _players = new List<Player>();

		public Player[] Players {
			get { return _players.ToArray(); }
		}

		public virtual int Id { get; set; }
		public virtual string Name { get; set; }
		public virtual int Expansion { get; set; }
		public virtual byte[] PasswordSalt { get; set; }
		public virtual byte[] PasswordVerifier { get; set; }
		public virtual byte[] SessionKey { get; set; }

		public Player GetPlayer(ulong guid) {
			return (from player in _players
			        where player.Guid == guid
			        select player).FirstOrDefault();
		}

		public void AddPlayer(Player player) {
			_players.Add(player);
		}

		public void DelPlayer(Player player) {
			_players.Remove(player);
		}
		
		#region packets

		public IPacket GetCharEnumPkt() {
			var p = new WorldPacket(WMSG.SMSG_CHAR_ENUM);
			var w = p.CreateWriter();
			w.Write((byte)Players.Count());
			foreach(var player in Players) {
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
				for(var i = 0; i < 20; i++) {
					var item = player.Items[i];
					if(item != null) {
						w.Write(0);
						w.Write((byte)0);
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
		
		public static IPacket GetCharCreatePkt(int error) {
			var responce = new WorldPacket(WMSG.SMSG_CHAR_CREATE);
			var w = responce.CreateWriter();
			w.Write((byte)error);
			return responce;
		}

		public static IPacket GetAccountDataTimesPkt() {
			var result = new WorldPacket(WMSG.SMSG_ACCOUNT_DATA_TIMES);
			var w = result.CreateWriter();
			w.Write(new byte[0x80]);
			return result;
		}

		public static IPacket GetCharacterLoginFiledPkt(int error) {
			var result = new WorldPacket(WMSG.SMSG_CHARACTER_LOGIN_FAILED);
			var w = result.CreateWriter();
			w.Write((byte)error);
			return result;
		}

		#endregion
	}
}
