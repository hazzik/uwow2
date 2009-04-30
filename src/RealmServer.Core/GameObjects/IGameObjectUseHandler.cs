using System;
using Hazzik.Objects;

namespace Hazzik.GameObjects {
	public interface IGameObjectUseHandler {
		bool Use(Player user);
	}

	public class ChairHandler : IGameObjectUseHandler {
		private readonly GameObject _go;

		public ChairHandler(GameObject go) {
			_go = go;
		}

		public bool Use(Player user) {
			user.PosX = _go.PosX;
			user.PosY = _go.PosY;
			user.PosZ = _go.PosZ;
			user.Facing = _go.Facing;
			user.StandState = StandStates.SittingChairLow + Hight;
			return true;
		}

		public int MaxCount {
			get { return (int)_go.Template.Fields[0]; }
		}

		public int Hight {
			get { return (int)_go.Template.Fields[1]; }
		}

		public bool Private {
			get { return _go.Template.Fields[2] != 0; }
		}
	}
}