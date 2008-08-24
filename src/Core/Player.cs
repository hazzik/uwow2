using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hazzik.Helper;
using System.IO;

namespace Hazzik {
	public class Player {
		public long Guid { get; private set; }
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
		public object[] Items = new object[20];
		public bool Dead;

		public Player() {
			Guid = ObjectGuid.NewGuid();
		}

		public void WriteSMSG_CHAR_ENUM(BinaryWriter w) {
			w.Write(this.Guid);
			w.WriteCString(this.Name);

			w.Write((byte)this.Race);
			w.Write((byte)this.Classe);
			w.Write((byte)this.Gender);
			w.Write(this.skin);
			w.Write(this.face);
			w.Write(this.hairStyle);
			w.Write(this.hairColor);
			w.Write(this.facialHair);
			w.Write((byte)this.level);

			w.Write((uint)this.ZoneId);
			w.Write((uint)this.MapId);
			w.Write(this.X);
			w.Write(this.Y);
			w.Write(this.Z);
			w.Write(this.GuildID);

			uint flag = 0x01000000;
			if(this.Dead)
				flag |= 0x20;
			w.Write(flag);
			w.Write((byte)1);
			w.Write(this.PetDisplayId);
			w.Write(this.PetLevel);
			w.Write(this.PetCreatureFamily);

			for(int i1 = 0; (i1 < 20); i1++) {
				if(this.Items[i1] == null) {
					w.Write(0);
					w.Write((byte)0);
					w.Write(0);
				}
				else {
					//w.Write(this.Items[i1].Model);
					//w.Write(((byte)this.Items[i1].InventoryType));
				}
			}
		}
	}
}
