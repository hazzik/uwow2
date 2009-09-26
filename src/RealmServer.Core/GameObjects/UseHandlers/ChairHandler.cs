using System;
using Hazzik.Objects;

namespace Hazzik.GameObjects.UseHandlers {
	internal class ChairHandler : IGameObjectUseHandler {
		private readonly GameObject gameObject;

		public ChairHandler(GameObject go) {
			gameObject = go;
		}

		public int MaxCount {
			get { return gameObject.Template.Field0; }
		}

		public int Hight {
			get { return gameObject.Template.Field1; }
		}

		public bool Private {
			get { return gameObject.Template.Field2 != 0; }
		}

		#region IGameObjectUseHandler Members

		public bool Use(Player user) {
			user.PosX = gameObject.PosX;
			user.PosY = gameObject.PosY;
			user.PosZ = gameObject.PosZ;
			user.Facing = gameObject.Facing;
			user.HeartBeat();
			user.StandState = StandStates.SittingChairLow + Hight;
			return true;
		}

		#endregion
	}
}