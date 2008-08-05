using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UWoW {
	public class ServerInfo {
		byte _type;
		public byte Type {
			get { return _type; }
			set { _type = value; }
		}

		byte _locked;
		public byte Locked {
			get { return _locked; }
			set { _locked = value; }
		}

		byte _status;
		public byte Status {
			get { return _status; }
			set { _status = value; }
		}

		string _name;
		public string Name {
			get { return _name; }
			set { _name = value; }
		}

		string _address;
		public string Address {
			get { return _address; }
			set { _address = value; }
		}

		float _population;
		public float Population {
			get { return _population; }
			set { _population = value; }
		}

		byte _charactersCount;
		public byte CharactersCount {
			get { return _charactersCount; }
			set { _charactersCount = value; }
		}

		byte _language;
		public byte Language {
			get { return _language; }
			set { _language = value; }
		}

		byte _unk;
		public byte Unk {
			get { return _unk; }
			set { _unk = value; }
		}
	}
}
