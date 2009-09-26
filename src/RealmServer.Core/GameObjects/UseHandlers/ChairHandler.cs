using System;
using Hazzik.Objects;

namespace Hazzik.GameObjects.UseHandlers {
	internal class ChairHandler : IGameObjectUseHandler {
		private readonly GameObject gameObject;

		public ChairHandler(GameObject go) {
			gameObject = go;
		}

		public bool Use(Player user) {
			user.PosX = gameObject.PosX;
			user.PosY = gameObject.PosY;
			user.PosZ = gameObject.PosZ;
			user.Facing = gameObject.Facing;
			user.HeartBeat();
			user.StandState = StandStates.SittingChairLow + Hight;
			return true;
		}

		public int MaxCount {
			get { return (int)gameObject.Template.Fields[0]; }
		}

		public int Hight {
			get { return (int)gameObject.Template.Fields[1]; }
		}

		public bool Private {
			get { return gameObject.Template.Fields[2] != 0; }
		}
	}
}