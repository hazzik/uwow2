using System;
using Hazzik.GameObjects;
using Hazzik.GameObjects.UseHandlers;
using Hazzik.Net;

namespace Hazzik.Objects {
	public partial class GameObject : Positioned {
		private readonly GameObjectTemplate _template;
		private readonly IGameObjectUseHandler _useHandler;

		private GameObject(GameObjectTemplate template) {
			Type |= ObjectTypes.GameObject;
			Entry = template.Id;
			GameObjectType = template.Type;
			DisplayId = template.DisplayId;
			ScaleX = template.ScaleX;
			_template = template;
			_useHandler = GetHandler();
		}

		public GameObjectTemplate Template {
			get { return _template; }
		}

		public override ObjectTypeId TypeId {
			get { return ObjectTypeId.GameObject; }
		}

		public virtual UInt64 Rotation { get; set; }

		public static GameObject Create(GameObjectTemplate template) {
			if(template == null) {
				return null;
			}
			return new GameObject(template);
		}

		public void Use(ISession client) {
			_useHandler.Use(client.Player);
		}

		private IGameObjectUseHandler GetHandler() {
			switch(GameObjectType) {
			case GameObjectType.Door:
				break;
			case GameObjectType.Button:
				break;
			case GameObjectType.QuestGiver:
				break;
			case GameObjectType.Chest:
				return new ChestUseHandler(this);
			case GameObjectType.Binder:
				break;
			case GameObjectType.Generic:
				break;
			case GameObjectType.Trap:
				break;
			case GameObjectType.Chair:
				return new ChairHandler(this);
			case GameObjectType.SpellFocus:
				break;
			case GameObjectType.Text:
				break;
			case GameObjectType.Goober:
				break;
			case GameObjectType.Transport:
				break;
			case GameObjectType.AreaDamage:
				break;
			case GameObjectType.Camera:
				break;
			case GameObjectType.MapObject:
				break;
			case GameObjectType.MapObjectTransport:
				break;
			case GameObjectType.DuelFlag:
				break;
			case GameObjectType.FishingNode:
				break;
			case GameObjectType.SummoningRitual:
				break;
			case GameObjectType.Mailbox:
				break;
			case GameObjectType.AuctionHouse:
				break;
			case GameObjectType.GuardPost:
				break;
			case GameObjectType.SpellCaster:
				break;
			case GameObjectType.MeetingStone:
				break;
			case GameObjectType.FlagStand:
				break;
			case GameObjectType.FishingHole:
				break;
			case GameObjectType.FlagDrop:
				break;
			case GameObjectType.MiniGame:
				break;
			case GameObjectType.LotteryKiosk:
				break;
			case GameObjectType.CapturePoint:
				break;
			case GameObjectType.AuraGenerator:
				break;
			case GameObjectType.DungeonDifficulty:
				break;
			case GameObjectType.BarberChair:
				break;
			case GameObjectType.DestructibleBuilding:
				break;
			case GameObjectType.GuildBank:
				break;
			case GameObjectType.TrapDoor:
				break;
			default:
				throw new ArgumentOutOfRangeException();
			}
			return new NullUseHandler();
		}
	}
}