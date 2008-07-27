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
			MemoryStream ms = new MemoryStream();
			IGenWriter gw = new GenericWriter(ms);

			gw.Write((byte)0x00);
			gw.Write((byte)0x03);
			gw.Write((ushort)0x29); //41
			gw.WriteString0("vrS");//4
			gw.Write((byte)1);		//1	
			gw.Write((byte)12);		//1
			gw.Write((byte)2);		//1
			gw.Write((ushort)6005);	//2
			gw.Write(Utility.Reverse(Encoding.UTF8.GetBytes("x86\0")));	//4
			gw.Write(Utility.Reverse(Encoding.UTF8.GetBytes("Win\0")));
			gw.Write(Utility.Reverse(Encoding.UTF8.GetBytes("enGB")));
			gw.Write(300);
			gw.Write((sock.RemoteEndPoint as IPEndPoint).Address.GetAddressBytes());
			gw.Write(username);
			gw.Close();

			sock.Send(ms.ToArray(), SocketFlags.None);
			byte[] b = new byte[65536];
			int n = sock.Receive(b, 65536, SocketFlags.None);
			ms = new MemoryStream(b, 0, n);

			IGenReader gr = new GenericReader(ms);
			gr.BaseStream.Seek(3, SeekOrigin.Begin);

			byte[] B = gr.ReadBytes(32);
			byte[] g = gr.ReadBytes(gr.ReadByte());
			byte[] N = gr.ReadBytes(gr.ReadByte());
			byte[] s = gr.ReadBytes(32);

			gr.Close();

			BigInteger bi_N = new BigInteger(Utility.Reverse(N));
			BigInteger bi_g = new BigInteger(Utility.Reverse(g));
			BigInteger bi_B = new BigInteger(Utility.Reverse(B));

			BigInteger bi_a = BigInteger.genPseudoPrime(160, 5, Utility.seed2) % bi_N;
			BigInteger bi_A = bi_g.modPow(bi_a, bi_N);

			byte[] A = Utility.Reverse(bi_A.getBytes());

			SHA1 sha1 = new SHA1Managed();

			byte[] u = sha1.ComputeHash(Utility.Concat(A, B));
			BigInteger bi_u = new BigInteger(Utility.Reverse(u));

			byte[] temp = sha1.ComputeHash(Encoding.UTF8.GetBytes("ADMIN:WPDIR4PA"));
			byte[] x = sha1.ComputeHash(Utility.Concat(s, temp));
			BigInteger bi_x = new BigInteger(Utility.Reverse(x));

			BigInteger temp1 = (bi_N * bi_k + bi_B - (bi_k * bi_g.modPow(bi_x, bi_N))) % bi_N;
			BigInteger temp2 = (bi_a + bi_u * bi_x);

			BigInteger bi_S = temp1.modPow(temp2, bi_N);

			byte[] S = Utility.Reverse(bi_S.getBytes());

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
			byte[] N_Hash = sha1.ComputeHash(N);
			byte[] G_Hash = sha1.ComputeHash(bi_g.getBytes());

			for (int i = 0; (i < 20); i++)
			{
				N_Hash[i] ^= G_Hash[i];
			}
			byte[] UserHash = sha1.ComputeHash(Encoding.UTF8.GetBytes("ADMIN"));
			byte[] Temp = Utility.Concat(N_Hash, UserHash);
			Temp = Utility.Concat(Temp, s);
			Temp = Utility.Concat(Temp, A);
			Temp = Utility.Concat(Temp, B);
			Temp = Utility.Concat(Temp, SS_Hash);
			byte[] M1 = sha1.ComputeHash(Temp);

			//BigInteger K = new BigInteger(sha1.ComputeHash(bi_S.getBytes()));

			ms = new MemoryStream();
			gw = new GenericWriter(ms);
			gw.Write((byte)1);
			gw.Write(A);
			gw.Write(M1);
			gw.Write(new byte[22]);
			gw.Close();

			sock.Send(ms.ToArray());

			//BigInteger Temp1 = new BigInteger(

			n = sock.Receive(b, 65536, SocketFlags.None);
			ms = new MemoryStream(b, 0, n);

			gr = new GenericReader(ms);
			gr.BaseStream.Seek(2, SeekOrigin.Begin);
			BigInteger bi_M2 = new BigInteger(gr.ReadBytes(20));

			Temp = Utility.Concat(A, M1);
			Temp = Utility.Concat(Temp, SS_Hash);
			byte[] M2Temp = sha1.ComputeHash(Temp);
			BigInteger bi_M2Temp = new BigInteger(M2Temp);
			Console.WriteLine(bi_M2 == bi_M2Temp);
			Console.ReadKey(true);
		}
	}
}
