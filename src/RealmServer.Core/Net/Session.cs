using System;
using System.IO;
using Hazzik.Creatures;
using Hazzik.Data;
using Hazzik.GameObjects;
using Hazzik.Gossip;
using Hazzik.IO;
using Hazzik.Items;
using Hazzik.Map;
using Hazzik.Objects;
using Hazzik.Objects.Update;

namespace Hazzik.Net {
	public class Session : ISession {
		private readonly IPacketSender packetSender;
		private UpdateManager updateManager;

		public Session(IPacketSender sender) {
			packetSender = sender;
		}

		#region ISession Members

		public Player Player { get; private set; }

		public Account Account { get; set; }

	    public void SendHeartBeat() {
			IPacket packet = WorldPacketFactory.Create(WMSG.MSG_MOVE_HEARTBEAT);
			BinaryWriter writer = packet.CreateWriter();
			writer.WritePackGuid(Player.Guid);
			Player.MovementInfo.Write(writer);
			SendNear(Player, packet);
		}

		public void SendNameQueryResponce(Player player) {
			Send(GetNameQueryResponcePkt(player));
		}

		public void SendRealmSplitPkt(uint unk1) {
			Send(GetRealmSplitPkt(unk1));
		}

		public void SendCharEnum() {
			Send(Account.GetCharEnumPkt());
		}

		public void SendCharCreate() {
			Send(GetCharCreatePkt(47));
		}

		public void SendAccountDataTimes(uint mask) {
			Send(GetAccountDataTimesPkt(mask));
		}

		public void SendLogoutComplete() {
			Send(GetLogoutCompletePkt());
		}

		public void SendLogoutResponce() {
			Send(GetLogoutResponcePkt(LogoutResponses.Accepted));
		}

		public void SendLogoutCancelAck() {
			Send(GetLogoutCancelAckPkt());
		}

		public void SendShowBank(ulong guid) {
			Send(GetShowBankPkt(guid));
		}

		public void SendDestroy(WorldObject item) {
			Send(GetDestroyObjectPkt(item));
		}

		public void SendCreatureQueryResponce(CreatureTemplate creature) {
			Send(GetCreatureQueryResponse(creature));
		}

		public void SendGameObjectQueryResponce(GameObjectTemplate template) {
			Send(GetGameObjectQueryResponcePkt(template));
		}

		public void SendItemQuerySingleResponse(ItemTemplate template) {
			Send(GetItemQuerySingleResponsePkt(template));
		}

		public void SendNpcTextUpdate(NpcTexts text) {
			Send(GetNpcTextUpdatePkt(text));
		}

		public void SendStandstateUpdate() {
			Send(new WorldPacket(WMSG.SMSG_STANDSTATE_UPDATE, new[] { (byte)Player.StandState }));
		}

		public void SendGossipMessage(ulong targetGuid, GossipMessage message) {
			Send(GetGossipMessagePkt(targetGuid, message));
		}

		public void Send(IPacketBuilder packetBuilder) {
			if(!packetBuilder.IsEmpty) {
				Send(packetBuilder.Build());
			}
		}

		public void LogOut() {
			ObjectManager.Remove(Player);
			updateManager.Stop();
			Player.Session = null;
			Player = null;
		}

		public void Login(Player player) {
			if(null == player) {
				SendCharacterLoginFiled();
				return;
			}

			Player = player;
			Player.Session = this;

			ObjectManager.Add(player);

			Creature creature = Creature.Create(IoC.Resolve<ICreatureTemplateRepository>().FindById(647));
			creature.PosX = player.PosX;
			creature.PosY = player.PosY;
			creature.PosZ = player.PosZ;
			creature.Health = 100;
			creature.MaxHealth = 100;
		    creature.NpcFlags = NpcFlags.Gossip | NpcFlags.QuestGiver | NpcFlags.Banker;
			ObjectManager.Add(creature);


			SendLoginVerifyWorld();
			SendAccountDataTimes(0xEA);
			SendLoginSetTimeSpeed();
			SendSetProficiency(2, -1);
			SendSetProficiency(4, -1);
			SendSetProficiency(6, -1);
			SendInitialSpells();

			updateManager = new UpdateManager(this);
			updateManager.Start();

			SendTimeSyncReq();
		}

        public void Send(IPacket packet) {
            try {
                packetSender.Send(packet);
            }
            catch(Exception) {
                LogOut();
            }
        }

	    #endregion

		private void SendInitialSpells() {
			Send(GetInitialSpellsPkt());
		}

		private void SendCharacterLoginFiled() {
			Send(GetCharacterLoginFiledPkt(0x44));
		}

		private void SendLoginVerifyWorld() {
			Send(GetLoginVerifyWorldPkt());
		}

		private IPacket GetLoginVerifyWorldPkt() {
			IPacket result = WorldPacketFactory.Create(WMSG.SMSG_LOGIN_VERIFY_WORLD);
			BinaryWriter writer = result.CreateWriter();
			writer.Write(Player.MapId);
			writer.Write(Player.PosX);
			writer.Write(Player.PosY);
			writer.Write(Player.PosZ);
			writer.Write(Player.Facing);
			return result;
		}

		private void SendLoginSetTimeSpeed() {
			Send(GetLoginSetTimeSpeedPkt());
		}

		private void SendTimeSyncReq() {
			Send(GetTimeSyncReqPkt());
		}

		private void SendSetProficiency(byte type, int bitmask) {
			Send(GetSetProficiencyPkt(type, bitmask));
		}

		private static void SendNear(Positioned me, IPacket responce) {
			foreach(Player player in ObjectManager.GetPlayersNear(me)) {
				if(player.Session != null) {
					player.Session.Send(responce);
				}
			}
		}

		public static void SendNearExceptMe(Positioned me, IPacket responce) {
			foreach(Player player in ObjectManager.GetPlayersNear(me)) {
				if(player.Session != null && player != me) {
				    player.Session.Send(responce);
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
				writer.Write((ushort)0);
			}
			writer.Write((ushort)0);
			return packet;
		}

		private static IPacket GetNameQueryResponcePkt(Player player) {
			IPacket result = WorldPacketFactory.Create(WMSG.SMSG_NAME_QUERY_RESPONSE);
			BinaryWriter writer = result.CreateWriter();
			writer.WritePackGuid(player.Guid);
			writer.Write((byte)0); // this is a type, ranging from 0-3
			writer.WriteCString(player.Name);
			writer.WriteCString("");
			writer.Write((byte)player.Race);
			writer.Write((byte)player.Gender);
			writer.Write((byte)player.Classe);
			writer.Write(true);
			writer.WriteCString(player.Name);
			writer.WriteCString(player.Name);
			writer.WriteCString(player.Name);
			writer.WriteCString(player.Name);
			return result;
		}

		private static IPacket GetRealmSplitPkt(uint unk1) {
			IPacket responce = WorldPacketFactory.Create(WMSG.SMSG_REALM_SPLIT);
			BinaryWriter w = responce.CreateWriter();
			w.Write(unk1);
			//0-normal, 1-split, 2-split pending;
			w.Write(0);
			w.WriteCString(DateTime.Now.AddDays(1).ToShortDateString());
			return responce;
		}

		private static IPacket GetCharCreatePkt(int error) {
			return new WorldPacket(WMSG.SMSG_CHAR_CREATE, new[] { (byte)error });
		}

		private static IPacket GetCharacterLoginFiledPkt(int error) {
			return new WorldPacket(WMSG.SMSG_CHARACTER_LOGIN_FAILED, new[] { (byte)error });
		}

		private IPacket GetAccountDataTimesPkt(uint mask) {
			IPacket packet = WorldPacketFactory.Create(WMSG.SMSG_ACCOUNT_DATA_TIMES);
			BinaryWriter writer = packet.CreateWriter();
			writer.Write(0);
			writer.Write((byte)1);
			writer.Write(mask);
			for(int i = 0; i < 8; i++) {
				if((mask & (1 << i)) != 0) {
					writer.Write(Account.FindAccpuntData((AccountDataType)i, Player != null ? Player.Guid : 0).Time.ToUnixTimestamp());
				}
			}
			return packet;
		}

		private static IPacket GetTimeSyncReqPkt() {
			return new WorldPacket(WMSG.SMSG_TIME_SYNC_REQ, new byte[4]);
		}

		private static IPacket GetLogoutCompletePkt() {
			return WorldPacketFactory.Create(WMSG.SMSG_LOGOUT_COMPLETE);
		}

		private static IPacket GetLogoutResponcePkt(LogoutResponses error) {
			IPacket result = WorldPacketFactory.Create(WMSG.SMSG_LOGOUT_RESPONSE);
			BinaryWriter writer = result.CreateWriter();
			writer.Write((byte)error);
			writer.Write(0);
			return result;
		}

		private static IPacket GetLogoutCancelAckPkt() {
			return WorldPacketFactory.Create(WMSG.SMSG_LOGOUT_CANCEL_ACK);
		}

		private static IPacket GetShowBankPkt(ulong guid) {
			IPacket responce = WorldPacketFactory.Create(WMSG.SMSG_SHOW_BANK);
			BinaryWriter writer = responce.CreateWriter();
			writer.Write(guid);
			return responce;
		}

		private static IPacket GetDestroyObjectPkt(WorldObject obj) {
			IPacket result = WorldPacketFactory.Create(WMSG.SMSG_DESTROY_OBJECT);
			BinaryWriter writer = result.CreateWriter();
			writer.Write(obj.Guid);
			return result;
		}

		private static IPacket GetCreatureQueryResponse(CreatureTemplate template) {
			IPacket packet = WorldPacketFactory.Create(WMSG.SMSG_CREATURE_QUERY_RESPONSE);
			BinaryWriter writer = packet.CreateWriter();
			writer.Write(template.Id);
			writer.WriteCString(template.Name);
			writer.WriteCString("");
			writer.WriteCString("");
			writer.WriteCString("");
			writer.WriteCString(template.GuildName);
			writer.WriteCString("");
			writer.Write((uint)template.Flags);
			writer.Write((uint)template.Type);
			writer.Write((uint)template.Family);
			writer.Write((uint)template.Rank);
			writer.Write(0); // SpellGroupId
			writer.Write(template.DisplayId);
			writer.Write(0);
			writer.Write(0);
			writer.Write(0);
			writer.Write(0);
			writer.Write(1f);
			writer.Write(1f);
			writer.Write((byte)0);
			writer.Write(0);
			writer.Write(0);
			writer.Write(0);
			writer.Write(0);
			writer.Write(0);
			writer.Write(0);
			writer.Write(0); // id from CreatureMovement.dbc
			return packet;
		}

		private static IPacket GetGameObjectQueryResponcePkt(GameObjectTemplate template) {
			IPacket packet = WorldPacketFactory.Create(WMSG.SMSG_GAMEOBJECT_QUERY_RESPONSE);
			BinaryWriter writer = packet.CreateWriter();
			writer.Write(template.Id);
			writer.Write((uint)template.Type);
			writer.Write(template.DisplayId);
			writer.WriteCString(template.Name);
			writer.WriteCString("");
			writer.WriteCString("");
			writer.WriteCString("");
			writer.WriteCString(template.IconName);
			writer.WriteCString(template.CastBarCaption);
			writer.WriteCString("");
			writer.Write(template.Field0);
			writer.Write(template.Field1);
			writer.Write(template.Field2);
			writer.Write(template.Field3);
			writer.Write(template.Field4);
			writer.Write(template.Field5);
			writer.Write(template.Field6);
			writer.Write(template.Field7);
			writer.Write(template.Field8);
			writer.Write(template.Field9);
			writer.Write(template.Field10);
			writer.Write(template.Field11);
			writer.Write(template.Field12);
			writer.Write(template.Field13);
			writer.Write(template.Field14);
			writer.Write(template.Field15);
			writer.Write(template.Field16);
			writer.Write(template.Field17);
			writer.Write(template.Field18);
			writer.Write(template.Field19);
			writer.Write(template.Field20);
			writer.Write(template.Field21);
			writer.Write(template.Field22);
			writer.Write(template.Field23);
			writer.Write(template.ScaleX);
			writer.Write(0);
			writer.Write(0);
			writer.Write(0);
			writer.Write(0);
			return packet;
		}

		private static IPacket GetItemQuerySingleResponsePkt(ItemTemplate template) {
			IPacket packet = WorldPacketFactory.Create(WMSG.SMSG_ITEM_QUERY_SINGLE_RESPONSE);
			BinaryWriter writer = packet.CreateWriter();
			writer.Write(template.Id);
			writer.Write(template.ObjectClass);
			writer.Write(template.SubClass);
			writer.Write(template.Unk1);
			writer.WriteCString(template.Name);
			writer.WriteCString(template.Name2);
			writer.WriteCString(template.Name3);
			writer.WriteCString(template.Name4);
			writer.Write(template.DisplayId);
			writer.Write(template.Quality);
			writer.Write(template.Flags);
			writer.Write(template.Faction);
			writer.Write(template.BuyPrice);
			writer.Write(template.SellPrice);
			writer.Write((int)template.InventoryType);
			writer.Write(template.RequiredClassMask);
			writer.Write(template.RequiredRaceMask);
			writer.Write(template.Level);
			writer.Write(template.RequiredLevel);
			writer.Write(template.RequiredSkill);
			writer.Write(template.RequiredSkillValue);
			writer.Write(template.RequiredSpell);
			writer.Write(template.RequiredPvPRank);
			writer.Write(template.RequiredPvPMedal);
			writer.Write(template.RequiredFaction);
			writer.Write(template.RequiredFactionStanding);
			writer.Write(template.UniqueCount);
			writer.Write(template.MaxAmount);
			writer.Write(template.ContainerSlots);

			writer.Write(10);
			for(int i = 0; i < 10; i++) {
				writer.Write(template.bonuses[i].Type);
				writer.Write(template.bonuses[i].Value);
			}

			writer.Write(0); // NEW 3.0.2 ScalingStatDistribution.dbc 
			writer.Write(0); // NEW 3.0.2 ScalingStatFlags

			for(int i = 0; i < 2; i++) {
				writer.Write(template.damages[i].Min);
				writer.Write(template.damages[i].Max);
				writer.Write(template.damages[i].School);
			}

			for(int i = 0; i < 7; i++) {
				writer.Write(template.Resistance[i]);
			}

			writer.Write(template.AttackTime); // 
			writer.Write(template.ProjectileType);
			writer.Write(template.RangeModifier);

			for(int i = 0; i < 5; i++) {
				writer.Write(template.spells[i].Id);
				writer.Write(template.spells[i].Trigger);
				writer.Write(template.spells[i].Charges);
				writer.Write(template.spells[i].Cooldown);
				writer.Write(template.spells[i].CategoryId);
				writer.Write(template.spells[i].CategoryCooldown);
			}

			writer.Write(template.BondType);
			writer.WriteCString(template.Description);
			writer.Write(template.PageTextId);
			writer.Write(template.PaeCount);
			writer.Write(template.PageMaterial);
			writer.Write(template.QuestId);
			writer.Write(template.LockId);
			writer.Write(template.Material);
			writer.Write(template.SheathType);
			writer.Write(template.RandomPropertiesId);
			writer.Write(template.RandomSuffixId);
			writer.Write(template.BlockValue);
			writer.Write(template.SetId);
			writer.Write(template.MaxDurability);
			writer.Write(template.ZoneId);
			writer.Write(template.MapId);
			writer.Write(template.BagFamily);
			writer.Write(template.TotemCategory);

			for(int i = 0; i < 3; i++) {
				writer.Write(template.sockets[i].Color);
				writer.Write(template.sockets[i].Content);
			}

			writer.Write(template.SocketBonusEnchantId);
			writer.Write(template.GemPropertiesId);
			writer.Write(template.RequiredDisenchantingLevel);
			writer.Write(template.ArmorModifier);
			writer.Write(0);
			writer.Write(0);
			return packet;
		}

		private static IPacket GetSetProficiencyPkt(byte type, int bitmask) {
			IPacket packet = WorldPacketFactory.Create(WMSG.SMSG_SET_PROFICIENCY);
			BinaryWriter writer = packet.CreateWriter();
			writer.Write(type);
			writer.Write(bitmask);
			return packet;
		}

		private static IPacket GetLoginSetTimeSpeedPkt() {
			IPacket result = WorldPacketFactory.Create(WMSG.SMSG_LOGIN_SETTIMESPEED);
			BinaryWriter w = result.CreateWriter();
			w.Write(WorldServerHandlers.GetActualTime());
			w.Write(0.01666667F);
			return result;
		}

		private static IPacket GetNpcTextUpdatePkt(NpcTexts texts) {
			IPacket responce = WorldPacketFactory.Create(WMSG.SMSG_NPC_TEXT_UPDATE);
			BinaryWriter writer = responce.CreateWriter();
			writer.Write(texts.Id);
			for(int i = 0; i < 8; i++) {
				NpcText text = texts.Texts[i];
				writer.Write(text.Probability);
				writer.WriteCString(text.Text0);
				writer.WriteCString(text.Text1);
				writer.Write(text.Language);
				for(int j = 0; (j < 3); j++) {
					writer.Write(text.Emote[j, 0]);
					writer.Write(text.Emote[j, 1]);
				}
			}
			return responce;
		}

		private static IPacket GetGossipMessagePkt(ulong guid, GossipMessage gossipMessage) {
			IPacket packet = WorldPacketFactory.Create(WMSG.SMSG_GOSSIP_MESSAGE);
			BinaryWriter writer = packet.CreateWriter();
			writer.Write(guid);
			writer.Write(0);
			writer.Write(gossipMessage.TextId);
			writer.Write(gossipMessage.GossipMenu.Count);
			foreach(GossipMenuItem menuItem in gossipMessage.GossipMenu) {
				writer.Write(menuItem.MenuId);
				writer.Write((byte)menuItem.Icon);
				writer.Write((byte)(menuItem.InputBox ? 1 : 0));
				writer.Write(menuItem.Cost);
				writer.WriteCString(menuItem.Text);
				writer.WriteCString(menuItem.AcceptText);
			}
			writer.Write(gossipMessage.QuestsMenu.Count);
			foreach(QuestsMenuItem menuItem in gossipMessage.QuestsMenu) {
				writer.Write(menuItem.Id);
				writer.Write(menuItem.Icon);
				writer.Write((uint)0);
				writer.Write(menuItem.Text);
			}

			return packet;
		}
	}
}