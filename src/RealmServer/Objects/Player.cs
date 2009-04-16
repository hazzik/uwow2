using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Hazzik.Dbc;
using Hazzik.Net;
using Hazzik.Objects.Templates;
using Hazzik.Objects.Update;
using Hazzik.Skills;

namespace Hazzik.Objects {
	public partial class Player : Unit, IContainer {
		private readonly IInventory _inventory;
		public bool Dead;
		public int PetCreatureFamily;
		public int PetDisplayId;
		public int PetLevel;
		private readonly IList<Skill> _skills = new List<Skill>();

		private readonly IList<int> _spells = new List<int>();

		public Player() {
			Type |= ObjectTypes.Player;
			_inventory = new PlayerInventory(this, UpdateFields.PLAYER_FARSIGHT - UpdateFields.PLAYER_FIELD_INV_SLOT_HEAD);
		}

		public override ObjectTypeId TypeId {
			get { return ObjectTypeId.Player; }
		}

		public override StandStates StandState {
			get { return base.StandState; }
			set {
				if(value == base.StandState) {
					return;
				}
				base.StandState = value;
				Client.Send(new WorldPacket(WMSG.SMSG_STANDSTATE_UPDATE, new[] { (byte)value }));
			}
		}

		public ISession Client { get; protected internal set; }

		#region IContainer Members

		WorldObject IContainer.Owner {
			get { return this; }
		}

		public IInventory Inventory {
			get { return _inventory; }
		}

		#endregion

		public void InitFake() {
			//return;
			//{X:-2133,334 Y:135,4584 Z:-9070,833}
			MapId = 0;
			PosX = -9070.833F;
			PosY = -2133.334F;
			PosZ = 135.4584f;
			Facing = 2.083644F;

			Speed0 = 2.5F;
			Speed1 = 7F;
			Speed2 = 4.5F;
			Speed3 = 4.722222F;
			Speed4 = 2.5F;
			Speed5 = 7F;
			Speed6 = 4.5F;
			TurnRate = 3.141593F;

			Race = Races.Orc;
			Classe = Classes.Warrior;
			Gender = GenderType.Male;
			PowerType = PowerType.Rage;
			Health = 50;
			Power = 100;
			Level = 80;
			DisplayId = (uint)(51 + Gender);
			NativeDisplayId = (uint)(51 + Gender);
			FactionTemplate = 0x0000065D;
			WatchedFactionIndex = -1;
			var abjurerSBoots9936 = new Abjurer_sBoots9936();
			Inventory[(int)abjurerSBoots9936.CanBeEquipedIn[0]] = ItemFactory.Create(abjurerSBoots9936);
			Inventory[Inventory.FindFreeSlot()] = ItemFactory.Create(new AncestralBoots3289());
			Inventory[Inventory.FindFreeSlot()] = ItemFactory.Create(new FelIronShells23772());
			Inventory[Inventory.FindFreeSlot()] = ItemFactory.Create(new FelIronShells23772());
			Inventory[Inventory.FindFreeSlot()] = ItemFactory.Create(new LargeRedSack857());
			Inventory[Inventory.FindFreeSlot()] = ItemFactory.Create(new LargeRedSack857());
			Inventory[Inventory.FindFreeSlot()] = ItemFactory.Create(new LargeRedSack857());
			Inventory[Inventory.FindFreeSlot()] = ItemFactory.Create(new LargeRedSack857());
			Inventory[Inventory.FindFreeSlot()] = ItemFactory.Create(new Abjurer_sRobe9943());

			TrainSpell(new[] {
				78, // Heroic Strike Rank 1
				81, // Dodge Passive
				107, // Block Passive
				196, // One-Handed Axes 
				198, // One-Handed Maces 
				201, // One-Handed Swords 
				203, // Unarmed 
				204, // Defense 
				522, // SPELLDEFENSE (DND) 
				668, // Language Common 
				2382, // Generic 
				2457, // Battle Stance 
				2479, // Honorless Target 
				3050, // Detect 
				3365, // Opening 
				5301, // Defensive State (DND) 
				6233, // Closing 
				6246, // Closing 
				6247, // Opening 
				6477, // Opening 
				6478, // Opening 
				6603, // Attack 
				7266, // Duel 
				7267, // Grovel 
				7355, // Stuck 
				8386, // Attacking 
				8737, // Mail 
				9077, // Leather 
				9078, // Cloth 
				9116, // Shield 
				9125, // Generic 
				20597, // Sword Specialization Racial Passive
				20598, // The Human Spirit Racial Passive
				20599, // Diplomacy Racial Passive
				20600, // Perception Racial
				20864, // Mace Specialization Racial Passive
				21651, // Opening 
				21652, // Closing 
				22027, // Remove Insignia 
				22810, // Opening - No Text 
				32215, // Victorious State
			});
			AddSkill(new Skill { Id = (ushort)SkillType.Protection, Value = 1, Cap = 1 });
			AddSkill(new Skill { Id = (ushort)SkillType.Arms, Value = 1, Cap = 1 });
			AddSkill(new Skill { Id = (ushort)SkillType.Fury, Value = 1, Cap = 1 });
			//armor skills
			AddSkill(new Skill { Id = (ushort)SkillType.Cloth, Value = 1, Cap = 1 });
			AddSkill(new Skill { Id = (ushort)SkillType.Leather, Value = 1, Cap = 1 });
			AddSkill(new Skill { Id = (ushort)SkillType.Mail, Value = 1, Cap = 1 });
			AddSkill(new Skill { Id = (ushort)SkillType.Shield, Value = 1, Cap = 1 });
		}

		public void TrainSpell(IEnumerable<int> spells) {
			foreach(var id in spells) {
				TrainSpell(id);
			}
		}

		public void AddSkill(Skill skill) {
			if(!HasSkill(skill.Id)) {
				_skills.Add(skill);
			}
		}

		public Skill GetSkill(ushort id) {
			return _skills.Where(s => s.Id == id).FirstOrDefault();
		}

		private bool HasSkill(ushort id) {
			return _skills.Where(s => s.Id == id).Any();
		}

		public IInventory GetInventory(int bag) {
			var container = bag == 0xff ? this : Inventory[bag] as IContainer;
			return container != null ? container.Inventory : null;
		}

		public IList<Skill> Skills {
			get { return _skills; }
		}

		public void TrainSpell(int spellId) {
			//var sla = new SkillLineAbilityRepository().FindBySpellId(spellId);
			//if(sla != null) {
			//   AddSkill(new Skill { Id = (ushort)sla.SkillId });
			//}
			_spells.Add(spellId);
		}

		public IPacket GetInitialSpellsPkt() {
			var packet = new WorldPacket(WMSG.SMSG_INITIAL_SPELLS);
			var writer = packet.CreateWriter();
			writer.Write((byte)0);
			writer.Write((ushort)_spells.Count);
			foreach(var i in _spells) {
				writer.Write(i);
			}
			writer.Write((ushort)0);
			return packet;
		}

		private void RemoveSpell(int spellId) {
			_spells.Remove(spellId);
		}

		private void RemoveSpellsAndSkill(ushort id) {
			foreach(var line in new SkillLineAbilityRepository().FindBySkillId(id)) {
				RemoveSpell(line.SpellId);
			}
		}
	}
}