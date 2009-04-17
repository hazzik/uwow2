using System;
using System.Security.Cryptography;
using Mono.Security.Cryptography;

namespace Hazzik.Cryptography {
	public class WowCryptRC4 : SymmetricAlgorithm {
		private static readonly HashAlgorithm _decryptorKeyHash = new HMACSHA1(new byte[] {
			0xF4, 0x66, 0x31, 0x59, 0xFC, 0x83, 0x6E, 0x31, 0x31, 0x2, 0x51, 0xD5, 0x44, 0x31, 0x67, 0x98
		});

		private static readonly HashAlgorithm _encryptorKeyHash = new HMACSHA1(new byte[] {
			0x22, 0xBE, 0xE5, 0xCF, 0xBB, 0x7, 0x64, 0xD9, 0x0, 0x45, 0x1B, 0xD0, 0x24, 0xB8, 0xD5, 0x45
		});


		public WowCryptRC4(byte[] sessionKey) {
			KeyValue = sessionKey;
			IVValue = new byte[0];
		}

		public override ICryptoTransform CreateEncryptor(byte[] rgbKey, byte[] rgbIV) {
			var serverEncryptor = new ARC4 { Key = _encryptorKeyHash.ComputeHash(rgbKey) };
			serverEncryptor.TransformFinalBlock(new byte[1024], 0, 1024);
			return serverEncryptor;
		}

		public override ICryptoTransform CreateDecryptor(byte[] rgbKey, byte[] rgbIV) {
			var clientDecryptor = new ARC4 { Key = _decryptorKeyHash.ComputeHash(rgbKey) };
			clientDecryptor.TransformFinalBlock(new byte[1024], 0, 1024);
			return clientDecryptor;
		}

		public override void GenerateKey() {
		}

		public override void GenerateIV() {
		}
	}
}