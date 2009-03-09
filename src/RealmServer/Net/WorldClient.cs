using System;
using System.IO;
using System.Net.Sockets;
using System.Security.Cryptography;
using Hazzik.Objects;

namespace Hazzik.Net {
	public class WorldClient : ClientBase {
		public RealmAccount Account { get; set; }

		private ICryptoTransform _decryptor;
		private ICryptoTransform _encryptor;

		private bool _firstPacket = true;
		private Player _player;
		public Player Player {
			get { return _player; }
			set { _player = value;
				_player.Client = this;
			}
		}

		private readonly Timer2 _updateTimer;

		public WorldClient(Socket socket)
			: base(socket) {
			_updateTimer = new UpdateTimer(this);
		}
		
		public void SetSymmetricAlgorithm(SymmetricAlgorithm algorithm) {
			_decryptor = algorithm.CreateDecryptor();
			_encryptor = algorithm.CreateEncryptor();
			_firstPacket = false;
		}

		public override IPacket ReadPacket() {
			var data = GetStream();
			var head = _firstPacket ? data : new CryptoStream(data, _decryptor, CryptoStreamMode.Read);

			var size = ReadSize(head);
			var code = ReadCode(head);

			var buffer = new byte[size - 4];
			data.Read(buffer, 0, buffer.Length);
			return new WorldPacket((WMSG)code, buffer);
		}

		public override void ReadPacketAsync(Action<IPacket> func) {
			var data = GetStream();
			var head = _firstPacket ? data : new CryptoStream(data, _decryptor, CryptoStreamMode.Read);

			var size = ReadSize(head);
			var code = ReadCode(head);

			var buffer = new byte[size - 4];
			data.ReadAsync(buffer, 0, buffer.Length, () => func(new WorldPacket((WMSG)code, buffer)));
		}

		public override void Send(IPacket packet) {
			var data = GetStream();
			var head = _firstPacket ? data : new CryptoStream(data, _encryptor, CryptoStreamMode.Write);
			WriteSize(head, packet);
			WriteCode(head, packet);
			packet.WriteBody(data);
		}

		private static int ReadCode(Stream stream) {
			var code = 0;
			code |= stream.ReadByte();
			code |= stream.ReadByte() << 0x08;
			code |= stream.ReadByte() << 0x10;
			code |= stream.ReadByte() << 0x18;
			return code;
		}
		
		private static void WriteCode(Stream head, IPacket packet) {
			head.WriteByte((byte)(packet.Code));
			head.WriteByte((byte)(packet.Code >> 0x08));
		}

		private static int ReadSize(Stream stream) {
			var size = stream.ReadByte();
			if((size & 0x80) != 0x00) {
				size &= 0x7f;
				size = (size << 0x08) | stream.ReadByte();
			}
			size = (size << 0x08) | stream.ReadByte();
			return size;
		}

		private static void WriteSize(Stream head, IPacket packet) {
			var size = packet.Size + 2;
			if(size > Int16.MaxValue) {
				head.WriteByte((byte)(size >> 0x10));
			}
			head.WriteByte((byte)(size >> 0x08));
			head.WriteByte((byte)(size));
		}

		public void StartUpdateTimer() {
			_updateTimer.Start();
		}

		public class UpdateTimer : Timer2 {
			private readonly WorldClient _client;

			public UpdateTimer(WorldClient client)
				: base(3000) {
				_client = client;
			}

			public override void OnTick() {
				if(_client.Player == null) {
					Stop();
					return;
				}
				_client.Send(_client.Player.GetUpdateObjectPkt());
			}
		}
	}
}