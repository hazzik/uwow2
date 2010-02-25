using System;
using System.Collections.Generic;
using System.Linq;
using Hazzik.Annotations;
using Hazzik.Dbc;
using Hazzik.Guilds;
using Hazzik.Items;
using Hazzik.Items.Inventories;
using Hazzik.Map;
using Hazzik.Net;
using Hazzik.Objects.Update;
using Hazzik.Skills;

namespace Hazzik.Objects {
	public partial class Player : Unit, IContainer {
		private readonly IInventory inventory;
		private readonly IList<QuestInfo> quests = new List<QuestInfo>();
		private readonly IList<Skill> skills = new List<Skill>();
		private readonly IList<int> spells = new List<int>();

		public int PetCreatureFamily;
		public int PetDisplayId;
		public int PetLevel;

		public Player() {
			Type |= ObjectTypes.Player;
			inventory = new PlayerInventory(this, (UpdateFields.PLAYER_FARSIGHT - UpdateFields.PLAYER_FIELD_INV_SLOT_HEAD) / 2);
			_equipment = new EquipmentInventory(this);
			_backPack = new BackPackInventory(this);
			_bank = new BankInventory(this);
			_bankBags = new BankBagsInventory(this);
			_keyRing = new KeyRingInventory(this);
		}

        public GuildMember GuildMember { get; set; }

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

	    public IList<QuestInfo> Quests {
			get { return quests; }
		}

		public IList<Skill> Skills {
			get { return skills; }
		}

		public IList<int> Spells {
			get { return spells; }
		}

		public override uint Health {
			get { return !Ghost ? base.Health : 1; }
			set { base.Health = value; }
		}

		public bool Ghost {
			get { return (Flags & PlayerFlags.Ghost) != 0; }
			set {
				if(value) {
					Flags |= PlayerFlags.Ghost;
				}
				else {
					Flags &= ~PlayerFlags.Ghost;
				}
			}
		}

		public Corpse Corpse { get; set; }

		#region IContainer Members

		WorldObject IContainer.Owner {
			get { return this; }
		}

		public IInventory Inventory {
			get { return inventory; }
		}

		#endregion

		public void TrainSpell(params int[] spellsForTrain) {
			foreach(int id in spellsForTrain) {
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

        public void TrainSpell(int spellId)
        {
            SkillLineAbility sla = new SkillLineAbilityRepository().FindBySpellId(spellId);
            if (sla != null)
            {
                var sl = new SkillLineRepository().FindById(sla.SkillId);
                AddSkill(sl.CreateSkill(this));
            }
            Spells.Add(spellId);
        }

	    private void RemoveSpell(int spellId) {
			Spells.Remove(spellId);
		}

		public void HeartBeat() {
			Session.SendHeartBeat();
		}

		public void Repop() {
			Corpse corpse = Corpse.Create(this);

			Corpse = corpse;
			ObjectManager.Add(corpse);

			Ghost = true;
		}

		public override bool IsSeenBy(Player unit) {
			if(Ghost && !unit.Ghost) {
				return false; //non ghost cant see ghost
			}
			return base.IsSeenBy(unit);
		}

        [NotNull]
        public QuestInfo GetQuestInfo(int questIndex) {
            if(questIndex < 0 && questIndex >= 25)
                throw new ArgumentOutOfRangeException("questIndex", questIndex, "questIndex must be between 0 and 25");
            if(questIndex < Quests.Count && Quests[questIndex] != null)
                return Quests[questIndex];
            return QuestInfo.Empty;
        }

	    [NotNull]
	    public Skill GetSkillAt(int skillIndex)
	    {
            if (Skills.Count <= skillIndex)
                return Skill.Empty;
	        return Skills[skillIndex];
	    }
	}

}