using System;
using System.IO;
using Hazzik.Net;

namespace Hazzik.GameObjects {
	public class GameObjectTemplate {
		public GameObjectTemplate() {
			Fields = new uint[24];
		}

		public uint Id { get; set; }
		public GameObjectType Type { get; set; }
		public uint DisplayId { get; set; }
		public string Name { get; set; }
		public uint[] Fields { get; set; }
		public float ScaleX { get; set; }

		public IPacket GetResponce() {
			IPacket packet = WorldPacketFactory.Create(WMSG.SMSG_GAMEOBJECT_QUERY_RESPONSE);
			BinaryWriter writer = packet.CreateWriter();
			writer.Write(Id);
			writer.Write((uint)Type);
			writer.Write(DisplayId);
			writer.WriteCString(Name);
			writer.WriteCString("");
			writer.WriteCString("");
			writer.WriteCString("");
			writer.WriteCString("");
			writer.WriteCString("");
			writer.WriteCString("");
			for(int i = 0; i < Fields.Length; i++) {
				writer.Write(Fields[i]);
			}
			writer.Write(ScaleX);
			writer.Write(0);
			writer.Write(0); 
			writer.Write(0);
			writer.Write(0);
			return packet;
		}
	}
}