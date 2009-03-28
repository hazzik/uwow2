using System;

namespace Hazzik.Objects.Update {
	public class UpdateObjectDto {
		private readonly WorldObject _obj;
		private readonly UpdateValue[] _values;

		public UpdateObjectDto(WorldObject obj) {
			_obj = obj;
			_values = new UpdateValue[GetMaxValues()];
			switch(_obj.TypeId) {
			case ObjectTypeId.Item:
				Update((Item)obj);
				break;
			case ObjectTypeId.Container:
				Update((Container)obj);
				break;
			case ObjectTypeId.Unit:
				Update((Unit)obj);
				break;
			case ObjectTypeId.Player:
				Update((Player)obj);
				break;
			case ObjectTypeId.GameObject:
				Update((GameObject)obj);
				break;
			case ObjectTypeId.DynamicObject:
				Update((DynamicObject)obj);
				break;
			case ObjectTypeId.Corpse:
				Update((Corpse)obj);
				break;
			case ObjectTypeId.Object:
			case ObjectTypeId.AIGroup:
			case ObjectTypeId.AreaTrigger:
				Update(obj);
				break;
			default:
				throw new ArgumentOutOfRangeException();
			}
		}

		public int MaxValues {
			get { return _values.Length; }
		}

		public virtual ulong Guid {
			get { return _obj.Guid; }
		}

		public WorldObject Object {
			get { return _obj; }
		}

		#region Send/Update

		protected internal uint GetValue(int field) {
		   return _values[(int)((UpdateFields)field)].UInt32;
		}

		protected internal void Set(UpdateFields field, uint value) {
			_values[(int)field].UInt32 = value;
		}

		protected internal void Set(UpdateFields field, int value) {
			Set(field, (uint)value);
		}

		protected internal void Set(UpdateFields field, ulong value) {
			Set(field, (uint)value);
			Set(field + 1, (uint)(value >> 32));
		}

		protected internal void Set(UpdateFields field, long value) {
			Set(field, (ulong)value);
		}

		protected internal void Set(UpdateFields field, float value) {
			_values[(int)field].Single = value;
		}

		protected internal void Set(UpdateFields field, ushort value0, ushort value1) {
			UpdateValue updateValue = _values[(int)field];
			updateValue.UInt16_0 = value0;
			updateValue.UInt16_1 = value1;
			_values[(int)field] = updateValue;
		}

		protected internal void Set(UpdateFields field, short value0, short value1) {
			Set(field, (ushort)value0, (ushort)value1);
		}
		
		protected internal void Set(UpdateFields field, byte byte0, byte byte1, byte byte2, byte byte3) {
			UpdateValue updateValue = _values[(int)field];
			updateValue.Uint8_0 = byte0;
			updateValue.Uint8_1 = byte1;
			updateValue.Uint8_2 = byte2;
			updateValue.Uint8_3 = byte3;
			_values[(int)field] = updateValue;
		}

		#endregion

		private int GetMaxValues() {
			switch(_obj.TypeId) {
			case ObjectTypeId.Object:
				return (int)UpdateFields.OBJECT_END;
			case ObjectTypeId.Item:
				return (int)UpdateFields.ITEM_END;
			case ObjectTypeId.Container:
				return (int)UpdateFields.CONTAINER_END;
			case ObjectTypeId.Unit:
				return (int)UpdateFields.UNIT_END;
			case ObjectTypeId.Player:
				return (int)UpdateFields.PLAYER_END;
			case ObjectTypeId.GameObject:
				return (int)UpdateFields.GAMEOBJECT_END;
			case ObjectTypeId.DynamicObject:
				return (int)UpdateFields.DYNAMICOBJECT_END;
			case ObjectTypeId.Corpse:
				return (int)UpdateFields.CORPSE_END;
			case ObjectTypeId.AIGroup:
				return (int)UpdateFields.OBJECT_END;
			case ObjectTypeId.AreaTrigger:
				return (int)UpdateFields.OBJECT_END;
			default:
				throw new ArgumentOutOfRangeException();
			}
		}

		public void Update(WorldObject obj) {
			Set(UpdateFields.OBJECT_FIELD_GUID, obj.Guid);
			Set(UpdateFields.OBJECT_FIELD_TYPE, (uint)obj.Type);
			Set(UpdateFields.OBJECT_FIELD_ENTRY, obj.Entry);
			Set(UpdateFields.OBJECT_FIELD_SCALE_X, obj.ScaleX);
		}

		public void Update(Item obj) {
			Update((WorldObject)obj);
			if(obj.Owner != null) {
				Set(UpdateFields.ITEM_FIELD_OWNER, obj.Owner.Guid);
			}
			if(obj.Contained != null) {
				Set(UpdateFields.ITEM_FIELD_CONTAINED, obj.Contained.Guid);
			}
			Set(UpdateFields.ITEM_FIELD_CREATOR, obj.Creator);
			Set(UpdateFields.ITEM_FIELD_GIFTCREATOR, obj.GiftCreator);
			Set(UpdateFields.ITEM_FIELD_STACK_COUNT, obj.StackCount);
			Set(UpdateFields.ITEM_FIELD_DURATION, obj.Duration);
			//Set(UpdateFields.ITEM_FIELD_SPELL_CHARGES,);
			Set(UpdateFields.ITEM_FIELD_FLAGS, obj.Flags);
			//Set(UpdateFields.ITEM_FIELD_ENCHANTMENT_1_1,);
			Set(UpdateFields.ITEM_FIELD_PROPERTY_SEED, obj.PropertySeed);
			Set(UpdateFields.ITEM_FIELD_RANDOM_PROPERTIES_ID, obj.RandomPropertiesId);
			Set(UpdateFields.ITEM_FIELD_ITEM_TEXT_ID, obj.ItemTextId);
			Set(UpdateFields.ITEM_FIELD_DURABILITY, obj.Durability);
			Set(UpdateFields.ITEM_FIELD_MAXDURABILITY, obj.MaxDurability);
		}

		public void Update(Container obj) {
			Update((Item)obj);
			Set(UpdateFields.CONTAINER_FIELD_NUM_SLOTS, obj.NumSlots);
			for(int i = 0; i < obj.NumSlots; i++) {
				Item item = obj.Inventory[i];
				if(item != null) {
					Set(UpdateFields.CONTAINER_FIELD_SLOT_1 + i * 2, item.Guid);
				}
			}
		}

		public void Update(GameObject obj) {
			Update((WorldObject)obj);
			Set(UpdateFields.OBJECT_FIELD_CREATED_BY, obj.CreatedByGuid);
			Set(UpdateFields.GAMEOBJECT_DISPLAYID, obj.DisplayId);
			Set(UpdateFields.GAMEOBJECT_FLAGS,
			          (ushort)obj.Flags,
			          (ushort)obj.FlagsHigh);
			Set(UpdateFields.GAMEOBJECT_ROTATION, obj.Rotation);
			Set(UpdateFields.GAMEOBJECT_PARENTROTATION, obj.ParentRotationX);
			Set(UpdateFields.GAMEOBJECT_PARENTROTATION + 1, obj.ParentRotationY);
			Set(UpdateFields.GAMEOBJECT_PARENTROTATION + 2, obj.ParentRotationZ);
			Set(UpdateFields.GAMEOBJECT_PARENTROTATION + 3, obj.ParentRotationO);
			Set(UpdateFields.GAMEOBJECT_POS_X, obj.PosX);
			Set(UpdateFields.GAMEOBJECT_POS_Y, obj.PosY);
			Set(UpdateFields.GAMEOBJECT_POS_Z, obj.PosZ);
			Set(UpdateFields.GAMEOBJECT_FACING, obj.Facing);
			Set(UpdateFields.GAMEOBJECT_DYNAMIC,
			          (ushort)obj.DynamicFlags,
			          (ushort)obj.DynamicFlagsHigh);
			Set(UpdateFields.GAMEOBJECT_FACTION, obj.Faction);
			Set(UpdateFields.GAMEOBJECT_LEVEL, obj.Level);
			Set(UpdateFields.GAMEOBJECT_BYTES_1,
			         (byte)obj.State,
			         (byte)obj.GameObjectType,
			         obj.ArtKit,
			         obj.AnimationProgress);
		}

		public void Update(DynamicObject obj) {
			Update((WorldObject)obj);
			Set(UpdateFields.DYNAMICOBJECT_CASTER, obj.CasterGuid);
			Set(UpdateFields.DYNAMICOBJECT_BYTES, obj.Bytes);
			Set(UpdateFields.DYNAMICOBJECT_SPELLID, obj.SpellId);
			Set(UpdateFields.DYNAMICOBJECT_RADIUS, obj.Radius);
			Set(UpdateFields.DYNAMICOBJECT_POS_X, obj.PosX);
			Set(UpdateFields.DYNAMICOBJECT_POS_Y, obj.PosY);
			Set(UpdateFields.DYNAMICOBJECT_POS_Z, obj.PosZ);
			Set(UpdateFields.DYNAMICOBJECT_CASTTIME, obj.Facing);
			Set(UpdateFields.DYNAMICOBJECT_CASTTIME, obj.CastTime);
		}

		public void Update(Corpse obj) {
			Update((WorldObject)obj);
			if(obj.Owner != null) {
				Set(UpdateFields.CORPSE_FIELD_OWNER, obj.Owner.Guid);
				//Set(UpdateFields.CORPSE_FIELD_PARTY, obj.Owner.Party);
			}
			Set(UpdateFields.CORPSE_FIELD_FACING, obj.Facing);
			Set(UpdateFields.CORPSE_FIELD_POS_X, obj.PosX);
			Set(UpdateFields.CORPSE_FIELD_POS_Y, obj.PosY);
			Set(UpdateFields.CORPSE_FIELD_POS_Z, obj.PosZ);
			Set(UpdateFields.CORPSE_FIELD_DISPLAY_ID, obj.DisplayId);
			if(obj.Owner != null) {
				IInventory inventory = obj.Owner.Inventory;
				for(int i = 0; i < 19; i++) {
					Set(UpdateFields.CORPSE_FIELD_ITEM + i, inventory[i].Entry);
				}
			}
			Set(UpdateFields.CORPSE_FIELD_BYTES_1,
			    obj.Bytes1_0,
			    (byte)obj.Race,
			    (byte)obj.Gender,
			    obj.Skin);
			Set(UpdateFields.CORPSE_FIELD_BYTES_2,
			    obj.Face,
			    obj.HairStyle,
			    obj.HairColor,
			    obj.Face);
			Set(UpdateFields.CORPSE_FIELD_GUILD, obj.GuildId);
			Set(UpdateFields.CORPSE_FIELD_FLAGS, (uint)obj.Flags);
			Set(UpdateFields.CORPSE_FIELD_DYNAMIC_FLAGS, (uint)obj.DynamicFlags);
		}

		public void Update(Unit obj) {
			Update((WorldObject)obj);
			Set(UpdateFields.UNIT_FIELD_CHARM, obj.CharmGuid);
			Set(UpdateFields.UNIT_FIELD_SUMMON, obj.SummonGuid);
			Set(UpdateFields.UNIT_FIELD_CRITTER, obj.CritterGuid);
			Set(UpdateFields.UNIT_FIELD_SUMMONEDBY, obj.SummonedByGuid);
			Set(UpdateFields.UNIT_FIELD_TARGET, obj.TargetGuid);
			Set(UpdateFields.UNIT_FIELD_CHANNEL_OBJECT, obj.ChannelObjectGuid);
			Set(UpdateFields.UNIT_FIELD_BYTES_0,
			    (byte)obj.Race,
			    (byte)obj.Classe,
			    (byte)obj.Gender,
			    (byte)obj.PowerType);
			Set(UpdateFields.UNIT_FIELD_HEALTH, obj.Health);
			Set(UpdateFields.UNIT_FIELD_POWER1, obj.Power);
			//Set(UpdateFields.UNIT_FIELD_POWER2, obj.Power2);
			//Set(UpdateFields.UNIT_FIELD_POWER3, obj.Power3);
			//Set(UpdateFields.UNIT_FIELD_POWER4, obj.Power4);
			//Set(UpdateFields.UNIT_FIELD_POWER5, obj.Power5);
			//Set(UpdateFields.UNIT_FIELD_POWER6, obj.Power6);
			//Set(UpdateFields.UNIT_FIELD_POWER7, obj.Power7);
			Set(UpdateFields.UNIT_FIELD_MAXHEALTH, obj.MaxHealth);
			Set(UpdateFields.UNIT_FIELD_MAXPOWER1, obj.MaxPower);
			//Set(UpdateFields.UNIT_FIELD_MAXPOWER2, obj.MaxPower1);
			//Set(UpdateFields.UNIT_FIELD_MAXPOWER3, obj.MaxPower2);
			//Set(UpdateFields.UNIT_FIELD_MAXPOWER4, obj.MaxPower3);
			//Set(UpdateFields.UNIT_FIELD_MAXPOWER5, obj.MaxPower4);
			//Set(UpdateFields.UNIT_FIELD_MAXPOWER6, obj.MaxPower5);
			//Set(UpdateFields.UNIT_FIELD_MAXPOWER6, obj.MaxPower6);
			Set(UpdateFields.UNIT_FIELD_POWER_REGEN_FLAT_MODIFIER, obj.PowerRegenFlatModifier);
			Set(UpdateFields.UNIT_FIELD_POWER_REGEN_INTERRUPTED_FLAT_MODIFIER, obj.PowerRegenInterruptedFlatModifier);
			Set(UpdateFields.UNIT_FIELD_LEVEL, obj.Level);
			Set(UpdateFields.UNIT_FIELD_FACTIONTEMPLATE, obj.FactionTemplate);
			//Set(UpdateFields.UNIT_VIRTUAL_ITEM_SLOT_ID,);
			Set(UpdateFields.UNIT_FIELD_FLAGS, obj.Flags);
			Set(UpdateFields.UNIT_FIELD_FLAGS_2, obj.Flags2);
			Set(UpdateFields.UNIT_FIELD_AURASTATE, obj.AuraState);
			//Set(UpdateFields.UNIT_FIELD_BASEATTACKTIME, obj.BaseAttackTime);
			//Set(UpdateFields.UNIT_FIELD_BASEATTACKTIME + 1, obj.OffHandAttackTime);
			Set(UpdateFields.UNIT_FIELD_RANGEDATTACKTIME, obj.RangedAttackTime);
			Set(UpdateFields.UNIT_FIELD_BOUNDINGRADIUS, obj.BoundingRadius);
			Set(UpdateFields.UNIT_FIELD_COMBATREACH, obj.CombatReach);
			Set(UpdateFields.UNIT_FIELD_DISPLAYID, obj.DisplayId);
			Set(UpdateFields.UNIT_FIELD_NATIVEDISPLAYID, obj.NativeDisplayId);
			Set(UpdateFields.UNIT_FIELD_MOUNTDISPLAYID, obj.MountDisplayId);
			Set(UpdateFields.UNIT_FIELD_MINDAMAGE, obj.MinDamage);
			Set(UpdateFields.UNIT_FIELD_MAXDAMAGE, obj.MaxDamage);
			Set(UpdateFields.UNIT_FIELD_MINOFFHANDDAMAGE, obj.MinOffHandDamage);
			Set(UpdateFields.UNIT_FIELD_MAXOFFHANDDAMAGE, obj.MaxOffHandDamage);
			Set(UpdateFields.UNIT_FIELD_BYTES_1,
			    (byte)obj.StandState,
			    obj.PetTalentPoints,
			    (byte)obj.StateFlags,
			    obj.UnitBytes1_3);
			Set(UpdateFields.UNIT_FIELD_PETNUMBER, obj.PetNumber);
			Set(UpdateFields.UNIT_FIELD_PET_NAME_TIMESTAMP, obj.PetNameTimestamp);
			Set(UpdateFields.UNIT_FIELD_PETEXPERIENCE, obj.PetXp);
			Set(UpdateFields.UNIT_FIELD_PETNEXTLEVELEXP, obj.PetNextLevelXp);
			Set(UpdateFields.UNIT_DYNAMIC_FLAGS, obj.DynamicFlags);
			Set(UpdateFields.UNIT_CHANNEL_SPELL, obj.ChannelSpell);
			Set(UpdateFields.UNIT_MOD_CAST_SPEED, obj.ModCastSpeed);
			Set(UpdateFields.UNIT_CREATED_BY_SPELL, obj.CreatedBySpell);
			Set(UpdateFields.UNIT_NPC_FLAGS, obj.NpcFlags);
			Set(UpdateFields.UNIT_NPC_EMOTESTATE, obj.NpcEmoteState);
			Set(UpdateFields.UNIT_FIELD_STAT0, obj.Stat0);
			Set(UpdateFields.UNIT_FIELD_STAT1, obj.Stat1);
			Set(UpdateFields.UNIT_FIELD_STAT2, obj.Stat2);
			Set(UpdateFields.UNIT_FIELD_STAT3, obj.Stat3);
			Set(UpdateFields.UNIT_FIELD_STAT4, obj.Stat4);
			Set(UpdateFields.UNIT_FIELD_POSSTAT0, obj.PosStat0);
			Set(UpdateFields.UNIT_FIELD_POSSTAT1, obj.PosStat1);
			Set(UpdateFields.UNIT_FIELD_POSSTAT2, obj.PosStat2);
			Set(UpdateFields.UNIT_FIELD_POSSTAT3, obj.PosStat3);
			Set(UpdateFields.UNIT_FIELD_POSSTAT4, obj.PosStat4);
			Set(UpdateFields.UNIT_FIELD_NEGSTAT0, obj.NegStat0);
			Set(UpdateFields.UNIT_FIELD_NEGSTAT1, obj.NegStat1);
			Set(UpdateFields.UNIT_FIELD_NEGSTAT2, obj.NegStat2);
			Set(UpdateFields.UNIT_FIELD_NEGSTAT3, obj.NegStat3);
			Set(UpdateFields.UNIT_FIELD_NEGSTAT4, obj.NegStat4);
			//Set(UpdateFields.UNIT_FIELD_RESISTANCES,);
			//Set(UpdateFields.UNIT_FIELD_RESISTANCEBUFFMODSPOSITIVE,);
			//Set(UpdateFields.UNIT_FIELD_RESISTANCEBUFFMODSNEGATIVE,);
			Set(UpdateFields.UNIT_FIELD_BASE_MANA, obj.BaseMana);
			Set(UpdateFields.UNIT_FIELD_BASE_HEALTH, obj.BaseHealth);
			Set(UpdateFields.UNIT_FIELD_BYTES_2,
			    (byte)obj.Sheath,
			    (byte)obj.PvpState,
			    (byte)obj.PetState,
			    (byte)obj.ShapeShiftForm);
			Set(UpdateFields.UNIT_FIELD_ATTACK_POWER, obj.AttackPower);
			Set(UpdateFields.UNIT_FIELD_ATTACK_POWER_MODS, obj.AttackPowerMods);
			Set(UpdateFields.UNIT_FIELD_ATTACK_POWER_MULTIPLIER, obj.AttackPowerMultiplier);
			Set(UpdateFields.UNIT_FIELD_RANGED_ATTACK_POWER, obj.RangedAttackPower);
			Set(UpdateFields.UNIT_FIELD_RANGED_ATTACK_POWER_MODS, obj.RangedAttackPowerMods);
			Set(UpdateFields.UNIT_FIELD_RANGED_ATTACK_POWER_MULTIPLIER, obj.RangedAttackPowerMultiplier);
			Set(UpdateFields.UNIT_FIELD_MINRANGEDDAMAGE, obj.MinRangedDamage);
			Set(UpdateFields.UNIT_FIELD_MAXRANGEDDAMAGE, obj.MaxRangedDamage);
			//Set(UpdateFields.UNIT_FIELD_POWER_COST_MODIFIER, );
			//Set(UpdateFields.UNIT_FIELD_POWER_COST_MULTIPLIER, );
			Set(UpdateFields.UNIT_FIELD_MAXHEALTHMODIFIER, obj.MaxHealthModifier);
			Set(UpdateFields.UNIT_FIELD_HOVERHEIGHT, obj.HoverHeight);
		}

		public void Update(Player obj) {
			Update((Unit)obj);
			Set(UpdateFields.PLAYER_DUEL_ARBITER, obj.DuelArbiterGuid);
			Set(UpdateFields.PLAYER_FLAGS, obj.Flags);
			Set(UpdateFields.PLAYER_GUILDID, obj.GuildId);
			Set(UpdateFields.PLAYER_GUILDRANK, obj.GuildRank);
			Set(UpdateFields.PLAYER_BYTES,
			    obj.Skin,
			    obj.Face,
			    obj.HairStyle,
			    obj.HairColor);
			Set(UpdateFields.PLAYER_BYTES_2,
			    obj.FacialHair,
			    obj.PlayerBytes2_2,
			    obj.BankBagSlots,
			    (byte)obj.RestState);
			Set(UpdateFields.PLAYER_BYTES_3,
			    (byte)obj.Gender,
			    obj.DrunkState,
			    obj.PlayerBytes3_3,
			    obj.PvPRank);
			Set(UpdateFields.PLAYER_DUEL_TEAM, obj.DuelTeam);
			Set(UpdateFields.PLAYER_GUILD_TIMESTAMP, obj.GuildTimestamp);
			//for(int i = 0; i < 25; i++) {
			//   Set(UpdateFields.PLAYER_QUEST_LOG_1_1, );
			//}
			for(int i = 0; i < 19; i++) {
				var item = obj.Inventory[i];
				if(null != item) {
					SetVisibleItem(UpdateFields.PLAYER_VISIBLE_ITEM_1_CREATOR + i * 19, item);
				}
			}
			Set(UpdateFields.PLAYER_CHOSEN_TITLE, obj.ChosenTitle);
			//PLAYER_FIELD_PAD_0 = UNIT_END + 453, // 1 1:Int 0:None
			//PLAYER_FIELD_INV_SLOT_HEAD = UNIT_END + 454, // 46 4:Long 2:Private
			//PLAYER_FIELD_PACK_SLOT_1 = UNIT_END + 500, // 32 4:Long 2:Private
			//PLAYER_FIELD_BANK_SLOT_1 = UNIT_END + 532, // 56 4:Long 2:Private
			//PLAYER_FIELD_BANKBAG_SLOT_1 = UNIT_END + 588, // 14 4:Long 2:Private
			//PLAYER_FIELD_VENDORBUYBACK_SLOT_1 = UNIT_END + 602, // 24 4:Long 2:Private
			//PLAYER_FIELD_KEYRING_SLOT_1 = UNIT_END + 626, // 64 4:Long 2:Private
			//PLAYER_FIELD_VANITYPET_SLOT_1 = UNIT_END + 690, // 36 4:Long 2:Private
			//PLAYER_FIELD_CURRENCYTOKEN_SLOT_1 = UNIT_END + 726, // 64 4:Long 2:Private
			//PLAYER_FIELD_QUESTBAG_SLOT_1 = UNIT_END + 790, // 64 4:Long 2:Private
			for(int i = 0; i < 400; i++) {
				var item = obj.Inventory[i];
				if(null != item) {
					Set(UpdateFields.PLAYER_FIELD_INV_SLOT_HEAD + i * 2, item.Guid);
				}
			}
			Set(UpdateFields.PLAYER_FARSIGHT, obj.FarSightGuid);
			Set(UpdateFields.PLAYER__FIELD_KNOWN_TITLES, obj.KnownTitlesGuid);
			Set(UpdateFields.PLAYER__FIELD_KNOWN_TITLES1, obj.KnownTitles1Guid);
			Set(UpdateFields.PLAYER_FIELD_KNOWN_CURRENCIES, obj.KnownCurrenciesGuid);
			Set(UpdateFields.PLAYER_XP, obj.Xp);
			Set(UpdateFields.PLAYER_NEXT_LEVEL_XP, obj.NextLevelXp);
			//PLAYER_SKILL_INFO_1_1 = UNIT_END + 864, // 384 2:Shorts 2:Private
			Set(UpdateFields.PLAYER_CHARACTER_POINTS1, obj.CharacterPoints1);
			Set(UpdateFields.PLAYER_CHARACTER_POINTS2, obj.CharacterPoints2);
			Set(UpdateFields.PLAYER_TRACK_CREATURES, obj.TrackCreatures);
			Set(UpdateFields.PLAYER_TRACK_RESOURCES, obj.TrackResources);
			Set(UpdateFields.PLAYER_BLOCK_PERCENTAGE, obj.BlockPercentage);
			Set(UpdateFields.PLAYER_DODGE_PERCENTAGE, obj.DodgePercentage);
			Set(UpdateFields.PLAYER_PARRY_PERCENTAGE, obj.ParryPercentage);
			Set(UpdateFields.PLAYER_EXPERTISE, obj.Expertise);
			Set(UpdateFields.PLAYER_OFFHAND_EXPERTISE, obj.OffHandExpertise);
			Set(UpdateFields.PLAYER_CRIT_PERCENTAGE, obj.CritPercentage);
			Set(UpdateFields.PLAYER_RANGED_CRIT_PERCENTAGE, obj.RangedCritPercentage);
			Set(UpdateFields.PLAYER_OFFHAND_CRIT_PERCENTAGE, obj.OffHandCritPercentage);
			//PLAYER_SPELL_CRIT_PERCENTAGE1 = UNIT_END + 1260, // 7 3:Single 2:Private
			Set(UpdateFields.PLAYER_SHIELD_BLOCK, obj.ShieldBlock);
			Set(UpdateFields.PLAYER_SHIELD_BLOCK_CRIT_PERCENTAGE, obj.ShieldBlockCritPercentage);
			//PLAYER_EXPLORED_ZONES_1 = UNIT_END + 1269, // 128 5:Bytes 2:Private
			Set(UpdateFields.PLAYER_REST_STATE_EXPERIENCE, obj.RestStateExperience);
			Set(UpdateFields.PLAYER_FIELD_COINAGE, obj.Coinage);
			//PLAYER_FIELD_MOD_DAMAGE_DONE_POS = UNIT_END + 1399, // 7 1:Int 2:Private
			//PLAYER_FIELD_MOD_DAMAGE_DONE_NEG = UNIT_END + 1406, // 7 1:Int 2:Private
			//PLAYER_FIELD_MOD_DAMAGE_DONE_PCT = UNIT_END + 1413, // 7 1:Int 2:Private
			Set(UpdateFields.PLAYER_FIELD_MOD_HEALING_DONE_POS, obj.ModHealingDonePos);
			Set(UpdateFields.PLAYER_FIELD_MOD_TARGET_RESISTANCE, obj.ModTargetResistance);
			Set(UpdateFields.PLAYER_FIELD_MOD_TARGET_PHYSICAL_RESISTANCE, obj.ModTargetPhysicalResistance);
			Set(UpdateFields.PLAYER_FIELD_BYTES, obj.FieldBytes);
			Set(UpdateFields.PLAYER_AMMO_ID, obj.AmmoId);
			Set(UpdateFields.PLAYER_SELF_RES_SPELL, obj.SelfResSpell);
			Set(UpdateFields.PLAYER_FIELD_PVP_MEDALS, obj.PvpMedals);
			//PLAYER_FIELD_BUYBACK_PRICE_1 = UNIT_END + 1427, // 12 1:Int 2:Private
			//PLAYER_FIELD_BUYBACK_TIMESTAMP_1 = UNIT_END + 1439, // 12 1:Int 2:Private
			Set(UpdateFields.PLAYER_FIELD_KILLS, obj.Kills);
			Set(UpdateFields.PLAYER_FIELD_TODAY_CONTRIBUTION, obj.TodayContribution);
			Set(UpdateFields.PLAYER_FIELD_YESTERDAY_CONTRIBUTION, obj.YesterdayContribution);
			Set(UpdateFields.PLAYER_FIELD_LIFETIME_HONORBALE_KILLS, obj.LifetimeHonorbaleKills);
			Set(UpdateFields.PLAYER_FIELD_BYTES2, obj.FieldBytes2);
			Set(UpdateFields.PLAYER_FIELD_WATCHED_FACTION_INDEX, obj.WatchedFactionIndex);
			//PLAYER_FIELD_COMBAT_RATING_1 = UNIT_END + 1457, // 25 1:Int 2:Private
			//PLAYER_FIELD_ARENA_TEAM_INFO_1_1 = UNIT_END + 1482, // 18 1:Int 2:Private
			Set(UpdateFields.PLAYER_FIELD_HONOR_CURRENCY, obj.HonorCurrency);
			Set(UpdateFields.PLAYER_FIELD_ARENA_CURRENCY, obj.ArenaCurrency);
			Set(UpdateFields.PLAYER_FIELD_MAX_LEVEL, obj.MaxLevel);
			//PLAYER_FIELD_DAILY_QUESTS_1 = UNIT_END + 1503, // 25 1:Int 2:Private
			//PLAYER_RUNE_REGEN_1 = UNIT_END + 1528, // 4 3:Single 2:Private
			//PLAYER_NO_REAGENT_COST_1 = UNIT_END + 1532, // 3 1:Int 2:Private
			//PLAYER_FIELD_GLYPH_SLOTS_1 = UNIT_END + 1535, // 8 1:Int 2:Private
			//PLAYER_FIELD_GLYPHS_1 = UNIT_END + 1543, // 8 1:Int 2:Private
			Set(UpdateFields.PLAYER_GLYPHS_ENABLED, obj.GlyphsEnabled);
		}

		private void SetVisibleItem(UpdateFields field, Item item) {
			Set(field, item.Creator);
			Set(field + 2, item.Entry);
		}
	}
}