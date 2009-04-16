using System;
using System.Collections.Generic;
using System.Linq;
using Hazzik.Dbc;
using Hazzik.Net;
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