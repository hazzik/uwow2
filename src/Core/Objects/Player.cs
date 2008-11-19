using System;
using System.Collections.Generic;

namespace Hazzik.Objects {
	public partial class Player : Unit {
		public Player()
			: this((int)UpdateFields.PLAYER_END, 0x19) {
			Guid = 0x0000000001B09FA7;
		}

		protected Player(int updateMaskLength, uint type)
			: base(updateMaskLength, type) {
		}

		public override byte TypeId {
			get { return (byte)ObjectTypeId.Player; }
		}

		public override byte UpdateFlag {
			get { return base.UpdateFlag; }
		}

		public string Name { get; set; }
		public Races Race { get; set; }
		public Classes Classe { get; set; }
		public int Gender { get; set; }
		public byte skin;
		public byte face;
		public byte hairStyle;
		public byte hairColor;
		public byte facialHair;
		public int level;
		public uint ZoneId;
		public uint MapId;
		public float X;
		public float Y;
		public float Z;
		public uint GuildID;
		public byte RestState;
		public int PetDisplayId;
		public int PetLevel;
		public int PetCreatureFamily;
		public Item[] Items = new Item[20];
		public bool Dead;

		private Dictionary<ulong, WorldObject> _knownObjects = new Dictionary<ulong, WorldObject>();

		public bool IsKnown(WorldObject obj) {
			if(_knownObjects.ContainsKey(obj.Guid)) {
				return true;
			}
			_knownObjects[obj.Guid] = obj;
			return false;
		}
	}
}