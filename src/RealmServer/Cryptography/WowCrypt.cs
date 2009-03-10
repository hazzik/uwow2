using System;
using System.Security.Cryptography;

namespace Hazzik.Cryptography {
	public class WowCrypt : SymmetricAlgorithm {
		private static readonly HashAlgorithm _hmac = new HMACSHA1(new byte[] {
			0x38, 0xA7, 0x83, 0x15, 0xF8, 0x92, 0x25, 0x30, 0x71, 0x98, 0x67, 0xB1, 0x8C, 0x4, 0xE2, 0xAA
		});

		public WowCrypt(byte[] key) {
			KeyValue = _hmac.ComputeHash(key);
			IVValue = new byte[] { 0 };
		}

		public override ICryptoTransform CreateDecryptor(byte[] rgbKey, byte[] rgbIV) {
			if(rgbIV.Length != 1) {
				throw new ArgumentException("IV should only be one byte long");
			}
			return new Transform(Direction.Decryption, rgbKey, rgbIV[0]);
		}

		public override ICryptoTransform CreateEncryptor(byte[] rgbKey, byte[] rgbIV) {
			if(rgbIV.Length != 1) {
				throw new ArgumentException("IV should only be one byte long");
			}
			return new Transform(Direction.Encryption, rgbKey, rgbIV[0]);
		}

		public override void GenerateIV() {
			throw new NotImplementedException();
		}

		public override void GenerateKey() {
			throw new NotImplementedException();
		}

		#region Nested type: Direction

		private enum Direction {
			Encryption,
			Decryption,
		}

		#endregion

		#region Nested type: Transform

		private class Transform : ICryptoTransform {
			protected readonly Direction _direction;
			protected readonly byte[] _key;
			protected byte _iv;
			protected int _keyPosition;

			internal Transform(Direction direction, byte[] key, byte iv) {
				_direction = direction;
				_key = key;
				_iv = iv;
			}

			#region ICryptoTransform Members

			public bool CanReuseTransform {
				get { return true; }
			}

			public bool CanTransformMultipleBlocks {
				get { return true; }
			}

			public int InputBlockSize {
				get { return 1; }
			}

			public int OutputBlockSize {
				get { return 1; }
			}

			public int TransformBlock(byte[] inputBuffer, int inputOffset, int inputCount, byte[] outputBuffer, int outputOffset) {
				lock(this) {
					int bytes = 0;

					for(long i = inputOffset, o = outputOffset;
						i < inputCount + inputOffset;
						i++, o++) {
						_keyPosition %= _key.Length;

						if(_direction == Direction.Encryption) {
							outputBuffer[o] = (byte)((inputBuffer[i] ^ _key[_keyPosition]) + _iv);
							_iv = outputBuffer[o];
						}
						else {
							outputBuffer[o] = (byte)((inputBuffer[i] - _iv) ^ _key[_keyPosition]);
							_iv = inputBuffer[i];
						}

						_keyPosition++;
						bytes++;
					}

					return bytes;
				}
			}

			public byte[] TransformFinalBlock(byte[] inputBuffer, int inputOffset, int inputCount) {
				var outputBuffer = new byte[inputCount];
				TransformBlock(inputBuffer, inputOffset, inputCount, outputBuffer, 0);
				return outputBuffer;
			}

			public void Dispose() {
				for(int i = 0; i < _key.Length; i++) {
					_key[i] = 0;
				}
				_iv = 0;
				_keyPosition = 0;
			}

			#endregion
		}

		#endregion
	}
}