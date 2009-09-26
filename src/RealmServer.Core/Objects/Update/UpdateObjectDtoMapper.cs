using System;
using Hazzik.Items;
using Hazzik.Skills;

namespace Hazzik.Objects.Update {
	internal static class UpdateObjectDtoMapper {
		public static void Update(UpdateValuesDto dto, WorldObject obj) {
			switch(obj.TypeId) {
			case ObjectTypeId.Item:
				UpdateItem(dto, (Item)obj);
				break;
			case ObjectTypeId.Container:
				UpdateContainer(dto, (Container)obj);
				break;
			case ObjectTypeId.Unit:
				UpdateUnit(dto, (Unit)obj);
				break;
			case ObjectTypeId.Player:
				UpdatePlayer(dto, (Player)obj);
				break;
			case ObjectTypeId.GameObject:
				UpdateGameObject(dto, (GameObject)obj);
				break;
			case ObjectTypeId.DynamicObject:
				UpdateDynamicObject(dto, (DynamicObject)obj);
				break;
			case ObjectTypeId.Corpse:
				UpdateCorpse(dto, (Corpse)obj);
				break;
			case ObjectTypeId.Object:
			case ObjectTypeId.AIGroup:
			case ObjectTypeId.AreaTrigger:
				UpdateObject(dto, obj);
				break;
			default:
				throw new ArgumentOutOfRangeException();
			}
		}

		private static void UpdateObject(UpdateValuesDto dto, WorldObject obj) {
			dto.Set(UpdateFields.OBJECT_FIELD_GUID, obj.Guid);
			dto.Set(UpdateFields.OBJECT_FIELD_TYPE, (uint)obj.Type);
			dto.Set(UpdateFields.OBJECT_FIELD_ENTRY, obj.Entry);
			dto.Set(UpdateFields.OBJECT_FIELD_SCALE_X, obj.ScaleX);
		}

		private static void UpdateItem(UpdateValuesDto dto, Item obj) {
			UpdateObject(dto, obj);
			if(obj.Owner != null) {
				dto.Set(UpdateFields.ITEM_FIELD_OWNER, obj.Owner.Guid);
			}
			else {
				dto.Set(UpdateFields.ITEM_FIELD_OWNER, (ulong)0);
			}
			if(obj.Contained != null) {
				dto.Set(UpdateFields.ITEM_FIELD_CONTAINED, obj.Contained.Guid);
			}
			else {
				dto.Set(UpdateFields.ITEM_FIELD_CONTAINED, (ulong)0);
			}
			dto.Set(UpdateFields.ITEM_FIELD_CREATOR, obj.Creator);
			dto.Set(UpdateFields.ITEM_FIELD_GIFTCREATOR, obj.GiftCreator);
			dto.Set(UpdateFields.ITEM_FIELD_STACK_COUNT, obj.StackCount);
			dto.Set(UpdateFields.ITEM_FIELD_DURATION, obj.Duration);
			//Set(UpdateFields.ITEM_FIELD_SPELL_CHARGES,);
			dto.Set(UpdateFields.ITEM_FIELD_FLAGS, obj.Flags);
			//Set(UpdateFields.ITEM_FIELD_ENCHANTMENT_1_1,);
			dto.Set(UpdateFields.ITEM_FIELD_PROPERTY_SEED, obj.PropertySeed);
			dto.Set(UpdateFields.ITEM_FIELD_RANDOM_PROPERTIES_ID, obj.RandomPropertiesId);
			dto.Set(UpdateFields.ITEM_FIELD_ITEM_TEXT_ID, obj.ItemTextId);
			dto.Set(UpdateFields.ITEM_FIELD_DURABILITY, obj.Durability);
			dto.Set(UpdateFields.ITEM_FIELD_MAXDURABILITY, obj.MaxDurability);
			dto.Set(UpdateFields.ITEM_FIELD_CREATE_PLAYED_TIME, obj.CreatePlayedTime);
		}

		private static void UpdateContainer(UpdateValuesDto dto, Container obj) {
			UpdateItem(dto, obj);
			dto.Set(UpdateFields.CONTAINER_FIELD_NUM_SLOTS, obj.NumSlots);
			for(int i = 0; i < obj.NumSlots; i++) {
				Item item = obj.Inventory[i];
				if(item != null) {
					dto.Set(UpdateFields.CONTAINER_FIELD_SLOT_1 + i * 2, item.Guid);
				}
				else {
					dto.Set(UpdateFields.CONTAINER_FIELD_SLOT_1 + i * 2, (ulong)0);
				}
			}
		}

		private static void UpdateGameObject(UpdateValuesDto dto, GameObject obj) {
			UpdateObject(dto, obj);
			dto.Set(UpdateFields.OBJECT_FIELD_CREATED_BY, obj.CreatedByGuid);
			dto.Set(UpdateFields.GAMEOBJECT_DISPLAYID, obj.DisplayId);
			dto.Set(UpdateFields.GAMEOBJECT_FLAGS, (uint)obj.Flags);
			dto.Set(UpdateFields.GAMEOBJECT_PARENTROTATION, obj.ParentRotationX);
			dto.Set(UpdateFields.GAMEOBJECT_PARENTROTATION + 1, obj.ParentRotationY);
			dto.Set(UpdateFields.GAMEOBJECT_PARENTROTATION + 2, obj.ParentRotationZ);
			dto.Set(UpdateFields.GAMEOBJECT_PARENTROTATION + 3, obj.ParentRotationO);
			dto.Set(UpdateFields.GAMEOBJECT_DYNAMIC,
			        (ushort)obj.DynamicFlags,
			        (ushort)obj.DynamicFlagsHigh);
			dto.Set(UpdateFields.GAMEOBJECT_FACTION, obj.Faction);
			dto.Set(UpdateFields.GAMEOBJECT_LEVEL, obj.Level);
			dto.Set(UpdateFields.GAMEOBJECT_BYTES_1,
			        (byte)obj.State,
			        (byte)obj.GameObjectType,
			        obj.ArtKit,
			        obj.AnimationProgress);
		}

		private static void UpdateDynamicObject(UpdateValuesDto dto, DynamicObject obj) {
			UpdateObject(dto, obj);
			dto.Set(UpdateFields.DYNAMICOBJECT_CASTER, obj.CasterGuid);
			dto.Set(UpdateFields.DYNAMICOBJECT_BYTES, obj.Bytes);
			dto.Set(UpdateFields.DYNAMICOBJECT_SPELLID, obj.SpellId);
			dto.Set(UpdateFields.DYNAMICOBJECT_RADIUS, obj.Radius);
			dto.Set(UpdateFields.DYNAMICOBJECT_CASTTIME, obj.CastTime);
		}

		private static void UpdateCorpse(UpdateValuesDto dto, Corpse obj) {
			UpdateObject(dto, obj);
			if(obj.Owner != null) {
				dto.Set(UpdateFields.CORPSE_FIELD_OWNER, obj.Owner.Guid);
				//dto.Set(UpdateFields.CORPSE_FIELD_PARTY, obj.Owner.Party);
			}
			dto.Set(UpdateFields.CORPSE_FIELD_DISPLAY_ID, obj.DisplayId);
			if(obj.Owner != null) {
				IInventory inventory = obj.Owner.Inventory;
				for(int i = 0; i < 19; i++) {
					if(null != inventory[i]) {
						dto.Set(UpdateFields.CORPSE_FIELD_ITEM + i, inventory[i].Entry);
					}
				}
			}
			dto.Set(UpdateFields.CORPSE_FIELD_BYTES_1,
			        obj.Bytes1_0,
			        (byte)obj.Race,
			        (byte)obj.Gender,
			        obj.Skin);
			dto.Set(UpdateFields.CORPSE_FIELD_BYTES_2,
			        obj.Face,
			        obj.HairStyle,
			        obj.HairColor,
			        obj.Face);
			dto.Set(UpdateFields.CORPSE_FIELD_GUILD, obj.GuildId);
			dto.Set(UpdateFields.CORPSE_FIELD_FLAGS, (uint)obj.Flags);
			dto.Set(UpdateFields.CORPSE_FIELD_DYNAMIC_FLAGS, (uint)obj.DynamicFlags);
		}

		private static void UpdateUnit(UpdateValuesDto dto, Unit obj) {
			UpdateObject(dto, obj);
			dto.Set(UpdateFields.UNIT_FIELD_CHARM, obj.CharmGuid);
			dto.Set(UpdateFields.UNIT_FIELD_SUMMON, obj.SummonGuid);
			dto.Set(UpdateFields.UNIT_FIELD_CRITTER, obj.CritterGuid);
			dto.Set(UpdateFields.UNIT_FIELD_SUMMONEDBY, obj.SummonedByGuid);
			dto.Set(UpdateFields.UNIT_FIELD_TARGET, obj.TargetGuid);
			dto.Set(UpdateFields.UNIT_FIELD_CHANNEL_OBJECT, obj.ChannelObjectGuid);
			dto.Set(UpdateFields.UNIT_FIELD_BYTES_0,
			        (byte)obj.Race,
			        (byte)obj.Classe,
			        (byte)obj.Gender,
			        (byte)obj.PowerType);
			dto.Set(UpdateFields.UNIT_FIELD_HEALTH, obj.Health);
			dto.Set(UpdateFields.UNIT_FIELD_POWER1, obj.Power);
			//Set(UpdateFields.UNIT_FIELD_POWER2, obj.Power2);
			//Set(UpdateFields.UNIT_FIELD_POWER3, obj.Power3);
			//Set(UpdateFields.UNIT_FIELD_POWER4, obj.Power4);
			//Set(UpdateFields.UNIT_FIELD_POWER5, obj.Power5);
			//Set(UpdateFields.UNIT_FIELD_POWER6, obj.Power6);
			//Set(UpdateFields.UNIT_FIELD_POWER7, obj.Power7);
			dto.Set(UpdateFields.UNIT_FIELD_MAXHEALTH, obj.MaxHealth);
			dto.Set(UpdateFields.UNIT_FIELD_MAXPOWER1, obj.MaxPower);
			//Set(UpdateFields.UNIT_FIELD_MAXPOWER2, obj.MaxPower1);
			//Set(UpdateFields.UNIT_FIELD_MAXPOWER3, obj.MaxPower2);
			//Set(UpdateFields.UNIT_FIELD_MAXPOWER4, obj.MaxPower3);
			//Set(UpdateFields.UNIT_FIELD_MAXPOWER5, obj.MaxPower4);
			//Set(UpdateFields.UNIT_FIELD_MAXPOWER6, obj.MaxPower5);
			//Set(UpdateFields.UNIT_FIELD_MAXPOWER6, obj.MaxPower6);
			dto.Set(UpdateFields.UNIT_FIELD_POWER_REGEN_FLAT_MODIFIER, obj.PowerRegenFlatModifier);
			dto.Set(UpdateFields.UNIT_FIELD_POWER_REGEN_INTERRUPTED_FLAT_MODIFIER, obj.PowerRegenInterruptedFlatModifier);
			dto.Set(UpdateFields.UNIT_FIELD_LEVEL, obj.Level);
			dto.Set(UpdateFields.UNIT_FIELD_FACTIONTEMPLATE, obj.FactionTemplate);
			//Set(UpdateFields.UNIT_VIRTUAL_ITEM_SLOT_ID,);
			dto.Set(UpdateFields.UNIT_FIELD_FLAGS, obj.Flags);
			dto.Set(UpdateFields.UNIT_FIELD_FLAGS_2, obj.Flags2);
			dto.Set(UpdateFields.UNIT_FIELD_AURASTATE, obj.AuraState);
			dto.Set(UpdateFields.UNIT_FIELD_BASEATTACKTIME, obj.BaseAttackTime);
			dto.Set(UpdateFields.UNIT_FIELD_BASEATTACKTIME + 1, obj.OffHandAttackTime);
			dto.Set(UpdateFields.UNIT_FIELD_RANGEDATTACKTIME, obj.RangedAttackTime);
			dto.Set(UpdateFields.UNIT_FIELD_BOUNDINGRADIUS, obj.BoundingRadius);
			dto.Set(UpdateFields.UNIT_FIELD_COMBATREACH, obj.CombatReach);
			dto.Set(UpdateFields.UNIT_FIELD_DISPLAYID, obj.DisplayId);
			dto.Set(UpdateFields.UNIT_FIELD_NATIVEDISPLAYID, obj.NativeDisplayId);
			dto.Set(UpdateFields.UNIT_FIELD_MOUNTDISPLAYID, obj.MountDisplayId);
			dto.Set(UpdateFields.UNIT_FIELD_MINDAMAGE, obj.MinDamage);
			dto.Set(UpdateFields.UNIT_FIELD_MAXDAMAGE, obj.MaxDamage);
			dto.Set(UpdateFields.UNIT_FIELD_MINOFFHANDDAMAGE, obj.MinOffHandDamage);
			dto.Set(UpdateFields.UNIT_FIELD_MAXOFFHANDDAMAGE, obj.MaxOffHandDamage);
			dto.Set(UpdateFields.UNIT_FIELD_BYTES_1,
			        (byte)obj.StandState,
			        obj.PetTalentPoints,
			        (byte)obj.StateFlags,
			        obj.UnitBytes1_3);
			dto.Set(UpdateFields.UNIT_FIELD_PETNUMBER, obj.PetNumber);
			dto.Set(UpdateFields.UNIT_FIELD_PET_NAME_TIMESTAMP, obj.PetNameTimestamp);
			dto.Set(UpdateFields.UNIT_FIELD_PETEXPERIENCE, obj.PetXp);
			dto.Set(UpdateFields.UNIT_FIELD_PETNEXTLEVELEXP, obj.PetNextLevelXp);
			dto.Set(UpdateFields.UNIT_DYNAMIC_FLAGS, obj.DynamicFlags);
			dto.Set(UpdateFields.UNIT_CHANNEL_SPELL, obj.ChannelSpell);
			dto.Set(UpdateFields.UNIT_MOD_CAST_SPEED, obj.ModCastSpeed);
			dto.Set(UpdateFields.UNIT_CREATED_BY_SPELL, obj.CreatedBySpell);
			dto.Set(UpdateFields.UNIT_NPC_FLAGS, (uint)obj.NpcFlags);
			dto.Set(UpdateFields.UNIT_NPC_EMOTESTATE, obj.NpcEmoteState);
			dto.Set(UpdateFields.UNIT_FIELD_STAT0, obj.Stat0);
			dto.Set(UpdateFields.UNIT_FIELD_STAT1, obj.Stat1);
			dto.Set(UpdateFields.UNIT_FIELD_STAT2, obj.Stat2);
			dto.Set(UpdateFields.UNIT_FIELD_STAT3, obj.Stat3);
			dto.Set(UpdateFields.UNIT_FIELD_STAT4, obj.Stat4);
			dto.Set(UpdateFields.UNIT_FIELD_POSSTAT0, obj.PosStat0);
			dto.Set(UpdateFields.UNIT_FIELD_POSSTAT1, obj.PosStat1);
			dto.Set(UpdateFields.UNIT_FIELD_POSSTAT2, obj.PosStat2);
			dto.Set(UpdateFields.UNIT_FIELD_POSSTAT3, obj.PosStat3);
			dto.Set(UpdateFields.UNIT_FIELD_POSSTAT4, obj.PosStat4);
			dto.Set(UpdateFields.UNIT_FIELD_NEGSTAT0, obj.NegStat0);
			dto.Set(UpdateFields.UNIT_FIELD_NEGSTAT1, obj.NegStat1);
			dto.Set(UpdateFields.UNIT_FIELD_NEGSTAT2, obj.NegStat2);
			dto.Set(UpdateFields.UNIT_FIELD_NEGSTAT3, obj.NegStat3);
			dto.Set(UpdateFields.UNIT_FIELD_NEGSTAT4, obj.NegStat4);
			//Set(UpdateFields.UNIT_FIELD_RESISTANCES,);
			//Set(UpdateFields.UNIT_FIELD_RESISTANCEBUFFMODSPOSITIVE,);
			//Set(UpdateFields.UNIT_FIELD_RESISTANCEBUFFMODSNEGATIVE,);
			dto.Set(UpdateFields.UNIT_FIELD_BASE_MANA, obj.BaseMana);
			dto.Set(UpdateFields.UNIT_FIELD_BASE_HEALTH, obj.BaseHealth);
			dto.Set(UpdateFields.UNIT_FIELD_BYTES_2,
			        (byte)obj.Sheath,
			        (byte)obj.PvpState,
			        (byte)obj.PetState,
			        (byte)obj.ShapeShiftForm);
			dto.Set(UpdateFields.UNIT_FIELD_ATTACK_POWER, obj.AttackPower.Base());
			dto.Set(UpdateFields.UNIT_FIELD_ATTACK_POWER_MODS, obj.AttackPowerMods);
			dto.Set(UpdateFields.UNIT_FIELD_ATTACK_POWER_MULTIPLIER, obj.AttackPowerMultiplier);
			dto.Set(UpdateFields.UNIT_FIELD_RANGED_ATTACK_POWER, obj.RangedAttackPower);
			dto.Set(UpdateFields.UNIT_FIELD_RANGED_ATTACK_POWER_MODS, obj.RangedAttackPowerMods);
			dto.Set(UpdateFields.UNIT_FIELD_RANGED_ATTACK_POWER_MULTIPLIER, obj.RangedAttackPowerMultiplier);
			dto.Set(UpdateFields.UNIT_FIELD_MINRANGEDDAMAGE, obj.MinRangedDamage);
			dto.Set(UpdateFields.UNIT_FIELD_MAXRANGEDDAMAGE, obj.MaxRangedDamage);
			//Set(UpdateFields.UNIT_FIELD_POWER_COST_MODIFIER, );
			//Set(UpdateFields.UNIT_FIELD_POWER_COST_MULTIPLIER, );
			dto.Set(UpdateFields.UNIT_FIELD_MAXHEALTHMODIFIER, obj.MaxHealthModifier);
			dto.Set(UpdateFields.UNIT_FIELD_HOVERHEIGHT, obj.HoverHeight);
		}

		private static void UpdatePlayer(UpdateValuesDto dto, Player obj) {
			//obj.Coinage = uint.MaxValue;
			UpdateUnit(dto, obj);
			dto.Set(UpdateFields.PLAYER_DUEL_ARBITER, obj.DuelArbiterGuid);
			dto.Set(UpdateFields.PLAYER_FLAGS, obj.Flags);
			dto.Set(UpdateFields.PLAYER_GUILDID, obj.GuildId);
			dto.Set(UpdateFields.PLAYER_GUILDRANK, obj.GuildRank);
			dto.Set(UpdateFields.PLAYER_BYTES,
			        obj.Skin,
			        obj.Face,
			        obj.HairStyle,
			        obj.HairColor);
			dto.Set(UpdateFields.PLAYER_BYTES_2,
			        obj.FacialHair,
			        obj.PlayerBytes2_2,
			        (byte)obj.BankBags.Slots,
			        (byte)obj.RestState);
			dto.Set(UpdateFields.PLAYER_BYTES_3,
			        (byte)obj.Gender,
			        obj.DrunkState,
			        obj.PlayerBytes3_3,
			        obj.PvPRank);
			dto.Set(UpdateFields.PLAYER_DUEL_TEAM, obj.DuelTeam);
			dto.Set(UpdateFields.PLAYER_GUILD_TIMESTAMP, obj.GuildTimestamp);
			//for(int i = 0; i < 25; i++) {
			//   Set(UpdateFields.PLAYER_QUEST_LOG_1_1, );
			//}
			for(int i = 0; i < 19; i++) {
				Item item = obj.Inventory[i];
				SetVisibleItem(dto, UpdateFields.PLAYER_VISIBLE_ITEM_1_ENTRYID + i * 2, item);
			}
			dto.Set(UpdateFields.PLAYER_CHOSEN_TITLE, obj.ChosenTitle);
			dto.Set(UpdateFields.PLAYER_FAKE_INEBRIATION, obj.FakeInebriation);
			//PLAYER_FIELD_INV_SLOT_HEAD = UNIT_END + 454, // 46 4:Long 2:Private
			//PLAYER_FIELD_PACK_SLOT_1 = UNIT_END + 500, // 32 4:Long 2:Private
			//PLAYER_FIELD_BANK_SLOT_1 = UNIT_END + 532, // 56 4:Long 2:Private
			//PLAYER_FIELD_BANKBAG_SLOT_1 = UNIT_END + 588, // 14 4:Long 2:Private
			//PLAYER_FIELD_VENDORBUYBACK_SLOT_1 = UNIT_END + 602, // 24 4:Long 2:Private
			//PLAYER_FIELD_KEYRING_SLOT_1 = UNIT_END + 626, // 64 4:Long 2:Private
			//PLAYER_FIELD_CURRENCYTOKEN_SLOT_1 = UNIT_END + 726, // 64 4:Long 2:Private
			for(int i = 0; i < obj.Inventory.Slots; i++) {
				Item item = obj.Inventory[i];
				if(null != item) {
					dto.Set(UpdateFields.PLAYER_FIELD_INV_SLOT_HEAD + i * 2, item.Guid);
				}
				else {
					dto.Set(UpdateFields.PLAYER_FIELD_INV_SLOT_HEAD + i * 2, (ulong)0);
				}
			}
			dto.Set(UpdateFields.PLAYER_FARSIGHT, obj.FarSightGuid);
			dto.Set(UpdateFields.PLAYER__FIELD_KNOWN_TITLES, obj.KnownTitleMask);
			dto.Set(UpdateFields.PLAYER__FIELD_KNOWN_TITLES1, obj.KnownTitleMask1);
			dto.Set(UpdateFields.PLAYER__FIELD_KNOWN_TITLES2, obj.KnownTitleMask2);
			dto.Set(UpdateFields.PLAYER_FIELD_KNOWN_CURRENCIES, obj.KnownCurrenciesGuid);
			dto.Set(UpdateFields.PLAYER_XP, obj.Xp);
			dto.Set(UpdateFields.PLAYER_NEXT_LEVEL_XP, obj.NextLevelXp);
			//PLAYER_SKILL_INFO_1_1 = UNIT_END + 864, // 384 2:Shorts 2:Private
			//obj.Skills.Add(new Skill() { Id = 43, Cap = 300, Value = 300 });
			for(int i = 0; i < 128; i++) {
				Skill skill;
				if((i < obj.Skills.Count) && (skill = obj.Skills[i]) != null) {
					dto.Set(UpdateFields.PLAYER_SKILL_INFO_1_1 + (i * 3), skill.Id, skill.Flags);
					dto.Set(UpdateFields.PLAYER_SKILL_INFO_1_1 + (i * 3) + 1, skill.Value, skill.Cap);
					dto.Set(UpdateFields.PLAYER_SKILL_INFO_1_1 + (i * 3) + 2, skill.Modifier, skill.Modifier2);
				}
				else {
					dto.Set(UpdateFields.PLAYER_SKILL_INFO_1_1 + (i * 3), 0);
					dto.Set(UpdateFields.PLAYER_SKILL_INFO_1_1 + (i * 3) + 1, 0);
					dto.Set(UpdateFields.PLAYER_SKILL_INFO_1_1 + (i * 3) + 2, 0);
				}
			}
			dto.Set(UpdateFields.PLAYER_CHARACTER_POINTS1, obj.CharacterPoints1);
			dto.Set(UpdateFields.PLAYER_CHARACTER_POINTS2, obj.CharacterPoints2);
			dto.Set(UpdateFields.PLAYER_TRACK_CREATURES, obj.TrackCreatures);
			dto.Set(UpdateFields.PLAYER_TRACK_RESOURCES, obj.TrackResources);
			dto.Set(UpdateFields.PLAYER_BLOCK_PERCENTAGE, obj.BlockPercentage);
			dto.Set(UpdateFields.PLAYER_DODGE_PERCENTAGE, obj.DodgePercentage);
			dto.Set(UpdateFields.PLAYER_PARRY_PERCENTAGE, obj.ParryPercentage);
			dto.Set(UpdateFields.PLAYER_EXPERTISE, obj.Expertise);
			dto.Set(UpdateFields.PLAYER_OFFHAND_EXPERTISE, obj.OffHandExpertise);
			dto.Set(UpdateFields.PLAYER_CRIT_PERCENTAGE, obj.CritPercentage);
			dto.Set(UpdateFields.PLAYER_RANGED_CRIT_PERCENTAGE, obj.RangedCritPercentage);
			dto.Set(UpdateFields.PLAYER_OFFHAND_CRIT_PERCENTAGE, obj.OffHandCritPercentage);
			//PLAYER_SPELL_CRIT_PERCENTAGE1 = UNIT_END + 1260, // 7 3:Single 2:Private
			dto.Set(UpdateFields.PLAYER_SHIELD_BLOCK, obj.ShieldBlock);
			dto.Set(UpdateFields.PLAYER_SHIELD_BLOCK_CRIT_PERCENTAGE, obj.ShieldBlockCritPercentage);
			//PLAYER_EXPLORED_ZONES_1 = UNIT_END + 1269, // 128 5:Bytes 2:Private
			dto.Set(UpdateFields.PLAYER_REST_STATE_EXPERIENCE, obj.RestStateExperience);
			dto.Set(UpdateFields.PLAYER_FIELD_COINAGE, obj.Coinage);
			//PLAYER_FIELD_MOD_DAMAGE_DONE_POS = UNIT_END + 1399, // 7 1:Int 2:Private
			//PLAYER_FIELD_MOD_DAMAGE_DONE_NEG = UNIT_END + 1406, // 7 1:Int 2:Private
			//PLAYER_FIELD_MOD_DAMAGE_DONE_PCT = UNIT_END + 1413, // 7 1:Int 2:Private
			dto.Set(UpdateFields.PLAYER_FIELD_MOD_HEALING_DONE_POS, obj.ModHealingDonePos);
			dto.Set(UpdateFields.PLAYER_FIELD_MOD_HEALING_PCT, obj.ModHealingPct);
			dto.Set(UpdateFields.PLAYER_FIELD_MOD_HEALING_DONE_PCT, obj.ModHealingDonePct);
			dto.Set(UpdateFields.PLAYER_FIELD_MOD_TARGET_RESISTANCE, obj.ModTargetResistance);
			dto.Set(UpdateFields.PLAYER_FIELD_MOD_TARGET_PHYSICAL_RESISTANCE, obj.ModTargetPhysicalResistance);
			dto.Set(UpdateFields.PLAYER_FIELD_BYTES, obj.FieldBytes);
			dto.Set(UpdateFields.PLAYER_AMMO_ID, obj.AmmoId);
			dto.Set(UpdateFields.PLAYER_SELF_RES_SPELL, obj.SelfResSpell);
			dto.Set(UpdateFields.PLAYER_FIELD_PVP_MEDALS, obj.PvpMedals);
			//PLAYER_FIELD_BUYBACK_PRICE_1 = UNIT_END + 1427, // 12 1:Int 2:Private
			//PLAYER_FIELD_BUYBACK_TIMESTAMP_1 = UNIT_END + 1439, // 12 1:Int 2:Private
			dto.Set(UpdateFields.PLAYER_FIELD_KILLS, obj.KillsToday, obj.KillsYesterday);
			dto.Set(UpdateFields.PLAYER_FIELD_TODAY_CONTRIBUTION, obj.TodayContribution);
			dto.Set(UpdateFields.PLAYER_FIELD_YESTERDAY_CONTRIBUTION, obj.YesterdayContribution);
			dto.Set(UpdateFields.PLAYER_FIELD_LIFETIME_HONORBALE_KILLS, obj.LifetimeHonorbaleKills);
			dto.Set(UpdateFields.PLAYER_FIELD_BYTES2, obj.FieldBytes2);
			dto.Set(UpdateFields.PLAYER_FIELD_WATCHED_FACTION_INDEX, obj.WatchedFactionIndex);
			//PLAYER_FIELD_COMBAT_RATING_1 = UNIT_END + 1457, // 25 1:Int 2:Private
			//PLAYER_FIELD_ARENA_TEAM_INFO_1_1 = UNIT_END + 1482, // 18 1:Int 2:Private
			dto.Set(UpdateFields.PLAYER_FIELD_HONOR_CURRENCY, obj.HonorCurrency);
			dto.Set(UpdateFields.PLAYER_FIELD_ARENA_CURRENCY, obj.ArenaCurrency);
			dto.Set(UpdateFields.PLAYER_FIELD_MAX_LEVEL, obj.MaxLevel);
			//PLAYER_FIELD_DAILY_QUESTS_1 = UNIT_END + 1503, // 25 1:Int 2:Private
			//PLAYER_RUNE_REGEN_1 = UNIT_END + 1528, // 4 3:Single 2:Private
			//PLAYER_NO_REAGENT_COST_1 = UNIT_END + 1532, // 3 1:Int 2:Private
			//PLAYER_FIELD_GLYPH_SLOTS_1 = UNIT_END + 1535, // 6 1:Int 2:Private
			//PLAYER_FIELD_GLYPHS_1 = UNIT_END + 1543, // 6 1:Int 2:Private
			dto.Set(UpdateFields.PLAYER_GLYPHS_ENABLED, obj.GlyphsEnabled);
		}

		private static void SetVisibleItem(UpdateValuesDto dto, UpdateFields field, Item item) {
			if(item != null) {
				dto.Set(field, item.Entry);
				dto.Set(field + 1, (ushort)0, 0);
			}
			else {
				dto.Set(field, (uint)0);
				dto.Set(field + 1, (ushort)0, 0);
			}
		}
	}
}