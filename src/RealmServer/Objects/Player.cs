using System;
using System.Collections;
using Hazzik.Net;
using Hazzik.Objects.Templates;

namespace Hazzik.Objects {
	public partial class Player : Unit, IContainer {
		private readonly IInventory _inventory;
		public bool Dead;
		public int PetCreatureFamily;
		public int PetDisplayId;
		public int PetLevel;

		public Player() {
			Type |= ObjectTypes.Player;
			_inventory = new PlayerInventory(this, UpdateFields.PLAYER_FARSIGHT - UpdateFields.PLAYER_FIELD_INV_SLOT_HEAD);
			InitFake();
		}

		public override ObjectTypeId TypeId {
			get { return ObjectTypeId.Player; }
		}

		public string Name { get; set; }

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

		private void InitFake() {
			MapId = 530;
			PosX = -3961.64F;
			PosY = -13931.2F;
			PosZ = 100.615F;
			Facing = 2.083644F;

			Speed0 = 2.5F;
			Speed1 = 7F;
			Speed2 = 4.5F;
			Speed3 = 4.722222F;
			Speed4 = 2.5F;
			Speed5 = 7F;
			Speed6 = 4.5F;
			TurnRate = 3.141593F;

			Race = Races.Draenei;
			Classe = Classes.Warrior;
			Gender = GenderType.Male;
			PowerType = PowerType.Rage;
			Health = 50;
			Power = 100;
			Level = 1;
			DisplayId = (uint)(16125 + Gender);
			FactionTemplate = 0x0000065D;
			var factory = new ItemFactory();
			var abjurer_sBoots9936 = new Abjurer_sBoots9936();
			Inventory[(int)abjurer_sBoots9936.CanBeEquipedIn[0]] = factory.Create(abjurer_sBoots9936);
			Inventory[Inventory.FindFreeSlot()] = factory.Create(new AncestralBoots3289());
			Inventory[Inventory.FindFreeSlot()] = factory.Create(new FelIronShells23772());
			Inventory[Inventory.FindFreeSlot()] = factory.Create(new FelIronShells23772());
			Inventory[Inventory.FindFreeSlot()] = factory.Create(new LargeRedSack857());
			Inventory[Inventory.FindFreeSlot()] = factory.Create(new LargeRedSack857());
			Inventory[Inventory.FindFreeSlot()] = factory.Create(new LargeRedSack857());
			Inventory[Inventory.FindFreeSlot()] = factory.Create(new LargeRedSack857());
		}

		public IInventory GetInventory(int bag) {
			var container = bag == 0xff ? this : Inventory[bag] as IContainer;
			return container!=null ? container.Inventory : null;
		}
	}
}