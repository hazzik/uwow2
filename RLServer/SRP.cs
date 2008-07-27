using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Security.Cryptography;
using System.Runtime.InteropServices;
using Helper;

namespace UWoW {
	public class SRP6 {
		private bool _isServer;
		private BigInteger _N = new BigInteger("B79B3E2A87823CAB8F5EBFBF8EB10108535006298B5BADBD5B53E1895E644B89", 16);
		private BigInteger _g = 7;
		private BigInteger _k = 3;
		private BigInteger _s;
		private string _I;
		private HashAlgorithm H = SHA1.Create();
		private BigInteger _u;
		private BigInteger _a; 
		private BigInteger _b;
		private BigInteger _A;
		private BigInteger _B;
		private BigInteger _x;
		private BigInteger _v;
		private BigInteger _S;
		
		public string I {
			get { return _I; }
			set { _I = value; }
		}
		public BigInteger N {
			get { return _N; }
			set { _N = value; }
		}
		public BigInteger A {
			get {
				if(_A == null) {
					CalculateA();
				}
				return _A;
			}
			set {
				if(_isServer) { _A = value; }
				else { throw new InvalidOperationException(); }
			}
		}
		public BigInteger B {
			get {
				if(_B == null) {
					CalculateB();
				}
				return _B;
			}
			set {
				if(_isServer) {
					throw new InvalidOperationException();
				}
				else {
					_B = value;
				}
			}
		}
		public BigInteger Salt {
			get { return _s; }
			set { _s = value; }
		}
		
		public SRP6(bool isServer) {
			_isServer = isServer;
			if(_isServer) {
				_b = BigInteger.genPseudoPrime(160, 5, Utility.seed2);
			}
			else {
				_a = BigInteger.genPseudoPrime(160, 5, Utility.seed2);
			}
		}

		public void CalculateX(string password) {
			var p = Encoding.UTF8.GetBytes((_I + ":" + password).ToUpper());
			var pHash = H.ComputeHash(p);
			var s = _s.getBytes().Reverse();
			var x = H.ComputeHash(s.Concat(pHash));
			_x = new BigInteger(x.Reverse());
		}
		public void CalculateV() {
			_v = _g.modPow(_x, _N);
		}
		public void CalculateA() {
			if(_isServer) {
				throw new InvalidOperationException();
			}
			else {
				_A = _g.modPow(_a, _N);
			}
		}
		public void CalculateB() {
			if(_isServer) {
				_B = (_k * _v + _g.modPow(_b, _N)) % _N;
			}
			else {
				throw new InvalidOperationException();
			}
		}
		public void CalculateU() {
			var A = _A.getBytes().Reverse();
			var B = _B.getBytes().Reverse();
			var u = H.ComputeHash(A.Concat(B));
			_u = new BigInteger(u.Reverse());
		}
		public void CalculateS() {
			if(_isServer) {
				_S = ((_A * _v.modPow(_u, _N)) % _N).modPow(_b, _N);
			}
			else {
				_S = (_B - _k * _g.modPow(_x, _N)).modPow(_a + _u * _x, _N);
			}
		}
		public void CalculateK(){

		}
		public void CalculateM2(byte[] m1){}
	}

}
