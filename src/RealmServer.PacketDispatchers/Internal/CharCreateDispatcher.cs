using System;
using System.IO;
using Hazzik.IO;
using Hazzik.Net;
using Hazzik.Objects;

namespace Hazzik.RealmServer.PacketDispatchers.Internal {
	[WorldPacketHandler(WMSG.CMSG_CHAR_CREATE)]
	internal class CharCreateDispatcher : IPacketDispatcher {
		#region IPacketDispatcher Members

		public void Dispatch(ISession session, IPacket packet) {
			Account account = session.Account;
			BinaryReader r = packet.CreateReader();
			var player = new Player {
				Name = r.ReadCString(),
				Race = (Races)r.ReadByte(),
				Classe = (Classes)r.ReadByte(),
				Gender = (GenderType)r.ReadByte(),
				Skin = r.ReadByte(),
				Face = r.ReadByte(),
				HairStyle = r.ReadByte(),
				HairColor = r.ReadByte(),
				FacialHair = r.ReadByte(),
			};
			new FakeCharacterCreateHandler(player).Init();
			account.AddPlayer(player);
			//Repository.Account.Save(account);
			//Repository.Account.SubmitChanges();
			session.SendCharCreate();
		}

		#endregion
	}
}