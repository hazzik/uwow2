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

		public Player()
			: base((int)UpdateFields.PLAYER_END) {
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

			SetUInt32((UpdateFields)22, 0x0100010B); // 22	UNIT_FIELD_HEALTH
			SetUInt32((UpdateFields)23, 0x00000032); // 23	UNIT_FIELD_POWER1
			SetUInt32((UpdateFields)26, 0x00000064); // 26	UNIT_FIELD_POWER4
			SetUInt32((UpdateFields)27, 0x00000064); // 27	UNIT_FIELD_POWER5
			SetUInt32((UpdateFields)29, 0x00000008); // 29	UNIT_FIELD_POWER7
			SetUInt32((UpdateFields)31, 0x00000032); // 31	UNIT_FIELD_MAXPOWER1
			SetUInt32((UpdateFields)33, 0x000003E8); // 33	UNIT_FIELD_MAXPOWER3
			SetUInt32((UpdateFields)34, 0x00000064); // 34	UNIT_FIELD_MAXPOWER4
			SetUInt32((UpdateFields)35, 0x00000064); // 35	UNIT_FIELD_MAXPOWER5
			SetUInt32((UpdateFields)37, 0x00000008); // 37	UNIT_FIELD_MAXPOWER7
			SetUInt32((UpdateFields)38, 0x000003E8); // 38	UNIT_FIELD_LEVEL
			SetUInt32((UpdateFields)53, 0x00000001); // 53	53
			SetUInt32((UpdateFields)54, 0x0000065D); // 54	54
			SetUInt32((UpdateFields)58, 0x00000008); // 58	58
			SetUInt32((UpdateFields)59, 0x00000800); // 59	59
			SetUInt32((UpdateFields)61, 0x00000B54); // 61	61
			SetUInt32((UpdateFields)62, 0x000007D0); // 62	62
			SetUInt32((UpdateFields)64, 0x3F800000); // 64	64
			SetUInt32((UpdateFields)65, 0x3FC00000); // 65	65
			SetUInt32((UpdateFields)66, 0x00003EFD); // 66	66
			SetUInt32((UpdateFields)67, 0x00003EFD); // 67	67
			SetUInt32((UpdateFields)69, 0x4116BE2C); // 69	69
			SetUInt32((UpdateFields)70, 0x4136BE2C); // 70	70
			SetUInt32((UpdateFields)80, 0x3F800000); // 80	80
			SetUInt32((UpdateFields)84, 0x00000018); // 84	84
			SetUInt32((UpdateFields)85, 0x00000011); // 85	85
			SetUInt32((UpdateFields)86, 0x00000015); // 86	86
			SetUInt32((UpdateFields)87, 0x00000015); // 87	87
			SetUInt32((UpdateFields)88, 0x00000016); // 88	88
			SetUInt32((UpdateFields)99, 0x00000024); // 99	99
			SetUInt32((UpdateFields)121, 0x00000014); // 121	121
			SetUInt32((UpdateFields)122, 0x11000000); // 122	UNIT_FIELD_AURALEVELS
			SetUInt32((UpdateFields)123, 0x0000001F); // 123	123
			SetUInt32((UpdateFields)126, 0x00000008); // 126	126
			SetUInt32((UpdateFields)146, 0x3F800000); // 146	146
			SetUInt32((UpdateFields)153, 0x02020505); // 153	UNIT_FIELD_RANGEDATTACKTIME
			SetUInt32((UpdateFields)154, 0x02000003); // 154	UNIT_FIELD_BOUNDINGRADIUS
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

		public BitArray GetRequeredMask(WorldObject obj) {
			var mask = new BitArray((int)UpdateFields.PLAYER_END);
			mask.SetAll(true);
			return mask;
		}

		public IInventory GetInventory(int bag) {
			var container = bag == 0xff ? this : Inventory[bag] as IContainer;
			return container!=null ? container.Inventory : null;
		}
	}
}