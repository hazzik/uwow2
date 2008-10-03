using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using Helper;
using System.Runtime.InteropServices;

namespace Core
{
	class Program
	{
		static BigInteger bi_k = 3;

		static void Main(string[] args)
		{
			Socket sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);
			sock.Connect("localhost", 3724);

			string username = "ADMIN";
			string password = "WPDIR4PA";
			using(var w = new BinaryWriter(new MemoryStream ())) {
				w.Write((byte)0x00);
				w.Write((byte)0x03);
				w.Write((ushort)0x29); //41
				w.Write(Encoding.UTF8.GetBytes("WoW"));
				w.Write((byte)0);
				w.Write((byte)1);		//1	
				w.Write((byte)12);		//1
				w.Write((byte)2);		//1
				w.Write((ushort)6005);	//2
				w.Write(Encoding.UTF8.GetBytes("x86").Reverse());	//4
				w.Write((byte)0);
				w.Write(Encoding.UTF8.GetBytes("Win").Reverse());
				w.Write((byte)0);
				w.Write(Encoding.UTF8.GetBytes("enGB").Reverse());
				w.Write(300);
				w.Write((sock.RemoteEndPoint as IPEndPoint).Address.GetAddressBytes());
				w.Write(username);
				sock.Send((w.BaseStream as MemoryStream).ToArray());
			}

			byte[] b = new byte[65536];
			int n = sock.Receive(b, 65536, SocketFlags.None);
			MemoryStream ms = new MemoryStream(b, 0, n);

			var gr = new BinaryReader(ms);
			gr.BaseStream.Seek(3, SeekOrigin.Begin);

			BigInteger bi_B = new BigInteger(gr.ReadBytes(32).Reverse());
			Console.WriteLine("B = " + bi_B.ToHexString());
			BigInteger bi_g = new BigInteger(gr.ReadBytes(gr.ReadByte()).Reverse());
			BigInteger bi_N = new BigInteger(gr.ReadBytes(gr.ReadByte()).Reverse());
			Console.WriteLine(bi_N.isProbablePrime());
			BigInteger bi_s = new BigInteger(gr.ReadBytes(32).Reverse());

			gr.Close();

			BigInteger bi_a = BigInteger.genPseudoPrime(160, 5, Utility.seed2) % bi_N;
			BigInteger bi_A = bi_g.modPow(bi_a, bi_N);
			Console.WriteLine("A = " + bi_A.ToHexString());

			SHA1 sha1 = new SHA1Managed();

			byte[] u = sha1.ComputeHash(Utility.Concat(bi_A.getBytes().Reverse(), bi_B.getBytes().Reverse()));
			BigInteger bi_u = new BigInteger(u.Reverse());
			Console.WriteLine("u= " + bi_u.ToHexString());

			byte[] temp = sha1.ComputeHash(Encoding.UTF8.GetBytes(username + ":" + password));
			byte[] x = sha1.ComputeHash(Utility.Concat(bi_s.getBytes().Reverse(), temp));
			BigInteger bi_x = new BigInteger(x.Reverse());

			BigInteger temp1 = (bi_N * bi_k + bi_B - (bi_k * bi_g.modPow(bi_x, bi_N))) % bi_N;
			BigInteger temp2 = (bi_a + bi_u * bi_x);

			BigInteger bi_S = temp1.modPow(temp2, bi_N);
			Console.WriteLine("S= " + bi_S.ToHexString());
			byte[] S = bi_S.getBytes().Reverse();

			byte[] S1 = new byte[16];
			byte[] S2 = new byte[16];
			byte[] SS_Hash = new byte[40];
			for (int i = 0; i < 16; i++)
			{
				S1[i] = S[i * 2];
				S2[i] = S[i * 2 + 1];
			}
			byte[] S1_Hash = sha1.ComputeHash(S1);
			byte[] S2_Hash = sha1.ComputeHash(S2);
			for (int i = 0; i < 20; i++)
			{
				SS_Hash[i * 2] = S1_Hash[i];
				SS_Hash[i * 2 + 1] = S2_Hash[i];
			}
			//this.myAccount.SS_Hash = SS_Hash;
			byte[] N_Hash = sha1.ComputeHash(bi_N.getBytes().Reverse());
			byte[] G_Hash = sha1.ComputeHash(bi_g.getBytes().Reverse());

			for (int i = 0; (i < 20); i++)
			{
				N_Hash[i] ^= G_Hash[i];
			}
			byte[] UserHash = sha1.ComputeHash(Encoding.UTF8.GetBytes("ADMIN"));
			byte[] Temp = Utility.Concat(N_Hash, UserHash);
			Temp = Utility.Concat(Temp, bi_s.getBytes().Reverse());
			Temp = Utility.Concat(Temp, bi_A.getBytes().Reverse());
			Temp = Utility.Concat(Temp, bi_B.getBytes().Reverse());
			Temp = Utility.Concat(Temp, SS_Hash);
			byte[] M1 = sha1.ComputeHash(Temp);

			//BigInteger K = new BigInteger(sha1.ComputeHash(bi_S.getBytes()));

			using(var w = new BinaryWriter(new MemoryStream())) {
				w.Write((byte)1);
				w.Write(bi_A.getBytes().Reverse());
				w.Write(M1);
				w.Write(new byte[22]);
				sock.Send((w.BaseStream as MemoryStream).ToArray());
			}

			//BigInteger Temp1 = new BigInteger(

			n = sock.Receive(b, 65536, SocketFlags.None);
			ms = new MemoryStream(b, 0, n);

			gr = new BinaryReader(ms);
			gr.BaseStream.Seek(2, SeekOrigin.Begin);
			BigInteger bi_M2 = new BigInteger(gr.ReadBytes(20));

			Temp = Utility.Concat(bi_A.getBytes().Reverse(), M1);
			Temp = Utility.Concat(Temp, SS_Hash);
			byte[] M2Temp = sha1.ComputeHash(Temp);
			BigInteger bi_M2Temp = new BigInteger(M2Temp);
			Console.WriteLine(bi_M2 == bi_M2Temp);
			Console.ReadKey(true);
		}
	}
}
