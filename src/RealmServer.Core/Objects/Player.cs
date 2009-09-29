using System;
using System.Collections.Generic;
using System.Linq;
using Hazzik.Dbc;
using Hazzik.Items;
using Hazzik.Items.Inventories;
using Hazzik.Map;
using Hazzik.Net;
using Hazzik.Objects.Update;
using Hazzik.Skills;

namespace Hazzik.Objects {
	public partial class Player : Unit, IContainer {
		private readonly IInventory _inventory;
		private readonly IList<Skill> _skills = new List<Skill>();

		private readonly IList<int> _spells = new List<int>();
		public bool Dead;
		public int PetCreatureFamily;
		public int PetDisplayId;
		public int PetLevel;
		private UpdateManager updateManager;

		public Player() {
			Type |= ObjectTypes.Player;
			_inventory = new PlayerInventory(this, (UpdateFields.PLAYER_FARSIGHT - UpdateFields.PLAYER_FIELD_INV_SLOT_HEAD) / 2);
			_equipment = new EquipmentInventory(this);
			_backPack = new BackPackInventory(this);
			_bank = new BankInventory(this);
			_bankBags = new BankBagsInventory(this);
			_keyRing = new KeyRingInventory(this);
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
				Session.SendStandstateUpdate();
			}
		}

		public ISession Session { get; protected internal set; }

		public IList<Skill> Skills {
			get { return _skills; }
		}

		public IList<int> Spells {
			get { return _spells; }
		}

		#region IContainer Members

		WorldObject IContainer.Owner {
			get { return this; }
		}

		public IInventory Inventory {
			get { return _inventory; }
		}

		#endregion

		public void TrainSpell(IEnumerable<int> spells) {
			foreach(int id in spells) {
				TrainSpell(id);
			}
		}

		public void AddSkill(Skill skill) {
			if(!HasSkill(skill.Id)) {
				Skills.Add(skill);
			}
		}

		public Skill GetSkill(ushort id) {
			return Skills.Where(s => s.Id == id).FirstOrDefault();
		}

		private bool HasSkill(ushort id) {
			return Skills.Where(s => s.Id == id).Any();
		}

		public IInventory GetInventory(int bag) {
			IContainer container = bag == 0xff ? this : Inventory[bag] as IContainer;
			return container != null ? container.Inventory : null;
		}

		public void TrainSpell(int spellId) {
			SkillLineAbility sla = new SkillLineAbilityRepository().FindBySpellId(spellId);
			if(sla != null) {
				AddSkill(new Skill { Id = (ushort)sla.SkillId,Value = 1,Cap = 1});
			}
			Spells.Add(spellId);
		}

		private void RemoveSpell(int spellId) {
			Spells.Remove(spellId);
		}

		public void HeartBeat() {
			Session.SendHeartBeat();
		}

		public void Logout() {
			ObjectManager.Remove(this);	
			updateManager.StopUpdateTimer();
		}

		public void SetUpdateManager(UpdateManager manager) {
			updateManager = manager;
		}
	}
}