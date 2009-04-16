using System;
using Hazzik.Net;

namespace Hazzik {
	public class ClientService {
		private readonly ISession _session;

		public ClientService(ISession client) {
			_session = client;
		}

		public void LogoutComplete() {
			_session.Player = null;
			_session.Client.Send(GetLogoutCompletePkt());
		}

		private static IPacket GetLogoutCompletePkt() {
			return WorldPacketFactory.Create(WMSG.SMSG_LOGOUT_COMPLETE);
		}
	}
}