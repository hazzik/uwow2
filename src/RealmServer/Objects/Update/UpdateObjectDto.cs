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

		protected internal void SetUInt32(UpdateFields field, uint value) {
			_values[(int)field].UInt32 = value;
		}

		protected internal void SetInt32(UpdateFields field, int value) {
			SetUInt32(field, (uint)value);
		}

		protected internal void SetUInt64(UpdateFields field, ulong value) {
			SetUInt32(field, (uint)value);
			SetUInt32(field + 1, (uint)(value >> 32));
		}

		protected internal void SetInt64(UpdateFields field, long value) {
			SetUInt64(field, (ulong)value);
		}

		protected internal void SetSingle(UpdateFields field, float value) {
			_values[(int)field].Single = value;
		}

		protected internal void SetUInt16(UpdateFields field, ushort value0, ushort value1) {
			UpdateValue updateValue = _values[(int)field];
			updateValue.UInt16_0 = value0;
			updateValue.UInt16_1 = value1;
			_values[(int)field] = updateValue;
		}

		protected internal void SetInt16(UpdateFields field, short value0, short value1) {
			SetUInt16(field, (ushort)value0, (ushort)value1);
		}
		
		protected internal void SetBytes(UpdateFields field, byte byte0, byte byte1, byte byte2, byte byte3) {
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
			SetUInt64(UpdateFields.OBJECT_FIELD_GUID, obj.Guid);
			SetUInt32(UpdateFields.OBJECT_FIELD_TYPE, (uint)obj.Type);
			SetUInt32(UpdateFields.OBJECT_FIELD_ENTRY, obj.Entry);
			SetSingle(UpdateFields.OBJECT_FIELD_SCALE_X, obj.ScaleX);
		}

		public void Update(Item obj) {
			Update((WorldObject)obj);
			if(obj.Owner != null) {
				SetUInt64(UpdateFields.ITEM_FIELD_OWNER, obj.Owner.Guid);
			}
			if(obj.Contained != null) {
				SetUInt64(UpdateFields.ITEM_FIELD_CONTAINED, obj.Contained.Guid);
			}
			SetUInt64(UpdateFields.ITEM_FIELD_CREATOR, obj.Creator);
			SetUInt64(UpdateFields.ITEM_FIELD_GIFTCREATOR, obj.GiftCreator);
			SetUInt32(UpdateFields.ITEM_FIELD_STACK_COUNT, obj.StackCount);
			SetUInt32(UpdateFields.ITEM_FIELD_DURATION, obj.Duration);
			//SetUInt32(UpdateFields.ITEM_FIELD_SPELL_CHARGES,);
			SetUInt32(UpdateFields.ITEM_FIELD_FLAGS, obj.Flags);
			//SetUInt32(UpdateFields.ITEM_FIELD_ENCHANTMENT_1_1,);
			SetUInt32(UpdateFields.ITEM_FIELD_PROPERTY_SEED, obj.PropertySeed);
			SetUInt32(UpdateFields.ITEM_FIELD_RANDOM_PROPERTIES_ID, obj.RandomPropertiesId);
			SetUInt32(UpdateFields.ITEM_FIELD_ITEM_TEXT_ID, obj.ItemTextId);
			SetUInt32(UpdateFields.ITEM_FIELD_DURABILITY, obj.Durability);
			SetUInt32(UpdateFields.ITEM_FIELD_MAXDURABILITY, obj.MaxDurability);
		}

		public void Update(Container obj) {
			Update((Item)obj);
			SetUInt32(UpdateFields.CONTAINER_FIELD_NUM_SLOTS, obj.NumSlots);
			for(int i = 0; i < obj.NumSlots; i++) {
				Item item = obj.Inventory[i];
				if(item != null) {
					SetUInt64(UpdateFields.CONTAINER_FIELD_SLOT_1 + i * 2, item.Guid);
				}
			}
		}

		public void Update(GameObject obj) {
			Update((WorldObject)obj);
			SetUInt64(UpdateFields.OBJECT_FIELD_CREATED_BY, obj.CreatedByGuid);
			SetUInt32(UpdateFields.GAMEOBJECT_DISPLAYID, obj.DisplayId);
			SetUInt16(UpdateFields.GAMEOBJECT_FLAGS,
			          (ushort)obj.Flags,
			          (ushort)obj.FlagsHigh);
			SetUInt64(UpdateFields.GAMEOBJECT_ROTATION, obj.Rotation);
			SetSingle(UpdateFields.GAMEOBJECT_PARENTROTATION, obj.ParentRotationX);
			SetSingle(UpdateFields.GAMEOBJECT_PARENTROTATION + 1, obj.ParentRotationY);
			SetSingle(UpdateFields.GAMEOBJECT_PARENTROTATION + 2, obj.ParentRotationZ);
			SetSingle(UpdateFields.GAMEOBJECT_PARENTROTATION + 3, obj.ParentRotationO);
			SetSingle(UpdateFields.GAMEOBJECT_POS_X, obj.PosX);
			SetSingle(UpdateFields.GAMEOBJECT_POS_Y, obj.PosY);
			SetSingle(UpdateFields.GAMEOBJECT_POS_Z, obj.PosZ);
			SetSingle(UpdateFields.GAMEOBJECT_FACING, obj.Facing);
			SetUInt16(UpdateFields.GAMEOBJECT_DYNAMIC,
			          (ushort)obj.DynamicFlags,
			          (ushort)obj.DynamicFlagsHigh);
			SetUInt32(UpdateFields.GAMEOBJECT_FACTION, obj.Faction);
			SetUInt32(UpdateFields.GAMEOBJECT_LEVEL, obj.Level);
			SetBytes(UpdateFields.GAMEOBJECT_BYTES_1,
			         (byte)obj.State,
			         (byte)obj.GameObjectType,
			         obj.ArtKit,
			         obj.AnimationProgress);
		}

		public void Update(DynamicObject obj) {
			Update((WorldObject)obj);
			SetUInt64(UpdateFields.DYNAMICOBJECT_CASTER, obj.CasterGuid);
			SetUInt32(UpdateFields.DYNAMICOBJECT_BYTES, obj.Bytes);
			SetUInt32(UpdateFields.DYNAMICOBJECT_SPELLID, obj.SpellId);
			SetSingle(UpdateFields.DYNAMICOBJECT_RADIUS, obj.Radius);
			SetSingle(UpdateFields.DYNAMICOBJECT_POS_X, obj.PosX);
			SetSingle(UpdateFields.DYNAMICOBJECT_POS_Y, obj.PosY);
			SetSingle(UpdateFields.DYNAMICOBJECT_POS_Z, obj.PosZ);
			SetSingle(UpdateFields.DYNAMICOBJECT_CASTTIME, obj.Facing);
			SetUInt32(UpdateFields.DYNAMICOBJECT_CASTTIME, obj.CastTime);
		}

		public void Update(Corpse obj) {
			Update((WorldObject)obj);
			if(obj.Owner != null) {
				SetUInt64(UpdateFields.CORPSE_FIELD_OWNER, obj.Owner.Guid);
				//SetUInt64(UpdateFields.CORPSE_FIELD_PARTY, obj.Owner.Party);
			}
			SetSingle(UpdateFields.CORPSE_FIELD_FACING, obj.Facing);
			SetSingle(UpdateFields.CORPSE_FIELD_POS_X, obj.PosX);
			SetSingle(UpdateFields.CORPSE_FIELD_POS_Y, obj.PosY);
			SetSingle(UpdateFields.CORPSE_FIELD_POS_Z, obj.PosZ);
			SetUInt32(UpdateFields.CORPSE_FIELD_DISPLAY_ID, obj.DisplayId);
			if(obj.Owner != null) {
				IInventory inventory = obj.Owner.Inventory;
				for(int i = 0; i < 19; i++) {
					SetUInt32(UpdateFields.CORPSE_FIELD_ITEM + i, inventory[i].Entry);
				}
			}
			SetBytes(UpdateFields.CORPSE_FIELD_BYTES_1,
			         obj.Bytes1_0,
			         (byte)obj.Race,
			         (byte)obj.Gender,
			         obj.Skin);
			SetBytes(UpdateFields.CORPSE_FIELD_BYTES_2,
			         obj.Face,
			         obj.HairStyle,
			         obj.HairColor,
			         obj.Face);
			SetUInt32(UpdateFields.CORPSE_FIELD_GUILD, obj.GuildId);
			SetUInt32(UpdateFields.CORPSE_FIELD_FLAGS, (uint)obj.Flags);
			SetUInt32(UpdateFields.CORPSE_FIELD_DYNAMIC_FLAGS, (uint)obj.DynamicFlags);
		}

		public void Update(Unit obj) {
			Update((WorldObject)obj);
			SetUInt64(UpdateFields.UNIT_FIELD_CHARM, obj.CharmGuid);
			SetUInt64(UpdateFields.UNIT_FIELD_SUMMON, obj.SummonGuid);
			SetUInt64(UpdateFields.UNIT_FIELD_CRITTER, obj.CritterGuid);
			SetUInt64(UpdateFields.UNIT_FIELD_SUMMONEDBY, obj.SummonedByGuid);
			SetUInt64(UpdateFields.UNIT_FIELD_TARGET, obj.TargetGuid);
			SetUInt64(UpdateFields.UNIT_FIELD_CHANNEL_OBJECT, obj.ChannelObjectGuid);
			SetBytes(UpdateFields.UNIT_FIELD_BYTES_0,
			         (byte)obj.Race,
			         (byte)obj.Classe,
			         (byte)obj.Gender,
			         (byte)obj.PowerType);
			SetUInt32(UpdateFields.UNIT_FIELD_HEALTH, obj.Health);
			SetUInt32(UpdateFields.UNIT_FIELD_POWER1, obj.Power);
			//SetUInt32(UpdateFields.UNIT_FIELD_POWER2, obj.Power2);
			//SetUInt32(UpdateFields.UNIT_FIELD_POWER3, obj.Power3);
			//SetUInt32(UpdateFields.UNIT_FIELD_POWER4, obj.Power4);
			//SetUInt32(UpdateFields.UNIT_FIELD_POWER5, obj.Power5);
			//SetUInt32(UpdateFields.UNIT_FIELD_POWER6, obj.Power6);
			//SetUInt32(UpdateFields.UNIT_FIELD_POWER7, obj.Power7);
			SetUInt32(UpdateFields.UNIT_FIELD_MAXHEALTH, obj.MaxHealth);
			SetUInt32(UpdateFields.UNIT_FIELD_MAXPOWER1, obj.MaxPower);
			//SetUInt32(UpdateFields.UNIT_FIELD_MAXPOWER2, obj.MaxPower1);
			//SetUInt32(UpdateFields.UNIT_FIELD_MAXPOWER3, obj.MaxPower2);
			//SetUInt32(UpdateFields.UNIT_FIELD_MAXPOWER4, obj.MaxPower3);
			//SetUInt32(UpdateFields.UNIT_FIELD_MAXPOWER5, obj.MaxPower4);
			//SetUInt32(UpdateFields.UNIT_FIELD_MAXPOWER6, obj.MaxPower5);
			//SetUInt32(UpdateFields.UNIT_FIELD_MAXPOWER6, obj.MaxPower6);
			SetSingle(UpdateFields.UNIT_FIELD_POWER_REGEN_FLAT_MODIFIER, obj.PowerRegenFlatModifier);
			SetSingle(UpdateFields.UNIT_FIELD_POWER_REGEN_INTERRUPTED_FLAT_MODIFIER, obj.PowerRegenInterruptedFlatModifier);
			SetUInt32(UpdateFields.UNIT_FIELD_LEVEL, obj.Level);
			SetUInt32(UpdateFields.UNIT_FIELD_FACTIONTEMPLATE, obj.FactionTemplate);
			//SetUInt32(UpdateFields.UNIT_VIRTUAL_ITEM_SLOT_ID,);
			SetUInt32(UpdateFields.UNIT_FIELD_FLAGS, obj.Flags);
			SetUInt32(UpdateFields.UNIT_FIELD_FLAGS_2, obj.Flags2);
			SetUInt32(UpdateFields.UNIT_FIELD_AURASTATE, obj.AuraState);
			//SetUInt32(UpdateFields.UNIT_FIELD_BASEATTACKTIME, obj.BaseAttackTime);
			//SetUInt32(UpdateFields.UNIT_FIELD_BASEATTACKTIME + 1, obj.OffHandAttackTime);
			SetUInt32(UpdateFields.UNIT_FIELD_RANGEDATTACKTIME, obj.RangedAttackTime);
			SetSingle(UpdateFields.UNIT_FIELD_BOUNDINGRADIUS, obj.BoundingRadius);
			SetSingle(UpdateFields.UNIT_FIELD_COMBATREACH, obj.CombatReach);
			SetUInt32(UpdateFields.UNIT_FIELD_DISPLAYID, obj.DisplayId);
			SetUInt32(UpdateFields.UNIT_FIELD_NATIVEDISPLAYID, obj.NativeDisplayId);
			SetUInt32(UpdateFields.UNIT_FIELD_MOUNTDISPLAYID, obj.MountDisplayId);
			SetSingle(UpdateFields.UNIT_FIELD_MINDAMAGE, obj.MinDamage);
			SetSingle(UpdateFields.UNIT_FIELD_MAXDAMAGE, obj.MaxDamage);
			SetSingle(UpdateFields.UNIT_FIELD_MINOFFHANDDAMAGE, obj.MinOffHandDamage);
			SetSingle(UpdateFields.UNIT_FIELD_MAXOFFHANDDAMAGE, obj.MaxOffHandDamage);
			SetBytes(UpdateFields.UNIT_FIELD_BYTES_1,
			         (byte)obj.StandState,
			         obj.PetTalentPoints,
			         (byte)obj.StateFlags,
			         obj.UnitBytes1_3);
			SetUInt32(UpdateFields.UNIT_FIELD_PETNUMBER, obj.PetNumber);
			SetUInt32(UpdateFields.UNIT_FIELD_PET_NAME_TIMESTAMP, obj.PetNameTimestamp);
			SetUInt32(UpdateFields.UNIT_FIELD_PETEXPERIENCE, obj.PetXp);
			SetUInt32(UpdateFields.UNIT_FIELD_PETNEXTLEVELEXP, obj.PetNextLevelXp);
			SetUInt32(UpdateFields.UNIT_DYNAMIC_FLAGS, obj.DynamicFlags);
			SetUInt32(UpdateFields.UNIT_CHANNEL_SPELL, obj.ChannelSpell);
			SetSingle(UpdateFields.UNIT_MOD_CAST_SPEED, obj.ModCastSpeed);
			SetUInt32(UpdateFields.UNIT_CREATED_BY_SPELL, obj.CreatedBySpell);
			SetUInt32(UpdateFields.UNIT_NPC_FLAGS, obj.NpcFlags);
			SetUInt32(UpdateFields.UNIT_NPC_EMOTESTATE, obj.NpcEmoteState);
			SetUInt32(UpdateFields.UNIT_FIELD_STAT0, obj.Stat0);
			SetUInt32(UpdateFields.UNIT_FIELD_STAT1, obj.Stat1);
			SetUInt32(UpdateFields.UNIT_FIELD_STAT2, obj.Stat2);
			SetUInt32(UpdateFields.UNIT_FIELD_STAT3, obj.Stat3);
			SetUInt32(UpdateFields.UNIT_FIELD_STAT4, obj.Stat4);
			SetUInt32(UpdateFields.UNIT_FIELD_POSSTAT0, obj.PosStat0);
			SetUInt32(UpdateFields.UNIT_FIELD_POSSTAT1, obj.PosStat1);
			SetUInt32(UpdateFields.UNIT_FIELD_POSSTAT2, obj.PosStat2);
			SetUInt32(UpdateFields.UNIT_FIELD_POSSTAT3, obj.PosStat3);
			SetUInt32(UpdateFields.UNIT_FIELD_POSSTAT4, obj.PosStat4);
			SetUInt32(UpdateFields.UNIT_FIELD_NEGSTAT0, obj.NegStat0);
			SetUInt32(UpdateFields.UNIT_FIELD_NEGSTAT1, obj.NegStat1);
			SetUInt32(UpdateFields.UNIT_FIELD_NEGSTAT2, obj.NegStat2);
			SetUInt32(UpdateFields.UNIT_FIELD_NEGSTAT3, obj.NegStat3);
			SetUInt32(UpdateFields.UNIT_FIELD_NEGSTAT4, obj.NegStat4);
			//SetUInt32(UpdateFields.UNIT_FIELD_RESISTANCES,);
			//SetUInt32(UpdateFields.UNIT_FIELD_RESISTANCEBUFFMODSPOSITIVE,);
			//SetUInt32(UpdateFields.UNIT_FIELD_RESISTANCEBUFFMODSNEGATIVE,);
			SetUInt32(UpdateFields.UNIT_FIELD_BASE_MANA, obj.BaseMana);
			SetUInt32(UpdateFields.UNIT_FIELD_BASE_HEALTH, obj.BaseHealth);
			SetBytes(UpdateFields.UNIT_FIELD_BYTES_2,
			         (byte)obj.Sheath,
			         (byte)obj.PvpState,
			         (byte)obj.PetState,
			         (byte)obj.ShapeShiftForm);
			SetUInt32(UpdateFields.UNIT_FIELD_ATTACK_POWER, obj.AttackPower);
			SetUInt32(UpdateFields.UNIT_FIELD_ATTACK_POWER_MODS, obj.AttackPowerMods);
			SetSingle(UpdateFields.UNIT_FIELD_ATTACK_POWER_MULTIPLIER, obj.AttackPowerMultiplier);
			SetUInt32(UpdateFields.UNIT_FIELD_RANGED_ATTACK_POWER, obj.RangedAttackPower);
			SetUInt32(UpdateFields.UNIT_FIELD_RANGED_ATTACK_POWER_MODS, obj.RangedAttackPowerMods);
			SetSingle(UpdateFields.UNIT_FIELD_RANGED_ATTACK_POWER_MULTIPLIER, obj.RangedAttackPowerMultiplier);
			SetSingle(UpdateFields.UNIT_FIELD_MINRANGEDDAMAGE, obj.MinRangedDamage);
			SetSingle(UpdateFields.UNIT_FIELD_MAXRANGEDDAMAGE, obj.MaxRangedDamage);
			//SetUInt32(UpdateFields.UNIT_FIELD_POWER_COST_MODIFIER, );
			//SetUInt32(UpdateFields.UNIT_FIELD_POWER_COST_MULTIPLIER, );
			SetSingle(UpdateFields.UNIT_FIELD_MAXHEALTHMODIFIER, obj.MaxHealthModifier);
			SetSingle(UpdateFields.UNIT_FIELD_HOVERHEIGHT, obj.HoverHeight);
		}

		public void Update(Player obj) {
			Update((Unit)obj);
			SetUInt64(UpdateFields.PLAYER_DUEL_ARBITER, obj.DuelArbiterGuid);
			SetUInt32(UpdateFields.PLAYER_FLAGS, obj.Flags);
			SetUInt32(UpdateFields.PLAYER_GUILDID, obj.GuildId);
		}
	}
}