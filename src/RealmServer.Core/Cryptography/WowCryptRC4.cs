using System;
using System.Security.Cryptography;
using Mono.Security.Cryptography;

namespace Hazzik.Cryptography {
	public class WowCryptRC4 : SymmetricAlgorithm {
		private static readonly HashAlgorithm decryptorKeyHash = new HMACSHA1(new byte[] {
			0xC2, 0xB3, 0x72, 0x3C, 0xC6, 0xAE, 0xD9, 0xB5, 0x34, 0x3C, 0x53, 0xEE, 0x2F, 0x43, 0x67, 0xCE
		});

		private static readonly HashAlgorithm encryptorKeyHash = new HMACSHA1(new byte[] {
			0xCC, 0x98, 0xAE, 0x04, 0xE8, 0x97, 0xEA, 0xCA, 0x12, 0xDD, 0xC0, 0x93, 0x42, 0x91, 0x53, 0x57
		});


		public WowCryptRC4(byte[] sessionKey) {
			KeyValue = sessionKey;
			IVValue = new byte[0];
		}

		public override ICryptoTransform CreateEncryptor(byte[] rgbKey, byte[] rgbIV) {
			var serverEncryptor = new ARC4 { Key = encryptorKeyHash.ComputeHash(rgbKey) };
			serverEncryptor.TransformFinalBlock(new byte[1024], 0, 1024);
			return serverEncryptor;
		}

		public override ICryptoTransform CreateDecryptor(byte[] rgbKey, byte[] rgbIV) {
			var clientDecryptor = new ARC4 { Key = decryptorKeyHash.ComputeHash(rgbKey) };
			clientDecryptor.TransformFinalBlock(new byte[1024], 0, 1024);
			return clientDecryptor;
		}

		public override void GenerateKey() {
		}

		public override void GenerateIV() {
		}
	}
}