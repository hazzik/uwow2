using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hazzik.Helper;
using System.IO;
using Hazzik.Objects;

namespace Hazzik.Objects {
	public class Player : Unit {
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

		public Player() {
			Guid = ObjectGuid.NewGuid();
		}

		public override void Accept(IObjectVisitor visitor) {
			visitor.Visit(this);
		}
	}
}
