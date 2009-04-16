using System;
using Hazzik.Net;

namespace Hazzik.Gossip {
	public class NpcTexts {
		public NpcTexts() {
		}

		public NpcTexts(uint id, NpcText[] texts) {
			Id = id;
			Texts = texts;
		}

		public NpcText[] Texts { get; private set; }

		public uint Id { get; private set; }

		public IPacket GetNpcTextUpdatePkt() {
			var responce = WorldPacketFactory.Create(WMSG.SMSG_NPC_TEXT_UPDATE);
			var writer = responce.CreateWriter();
			writer.Write(Id);
			for(var i = 0; i < 8; i++) {
				var text = Texts[i];
				writer.Write(text.Probability);
				writer.WriteCString(text.Text0);
				writer.WriteCString(text.Text1);
				writer.Write(text.Language);
				for(int j = 0; (j < 3); j++) {
					writer.Write(text.Emote[j, 0]);
					writer.Write(text.Emote[j, 1]);
				}
			}
			return responce;
		}
	}
}