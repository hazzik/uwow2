using HelperTools;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Threading;
using System.Collections;
using System.Runtime.InteropServices;
using System.Text;

namespace UWoW
{
	public class HeaderCoder
	{
		private byte[] SS = new byte[40];
		private int header_length;
		private int d_pointer;
		private int e_pointer;
		private byte d_last_ch;
		private byte e_last_ch;

		public int HeaderLength { get { return header_length; } }

		public HeaderCoder(byte[] ss, int length)
		{
			SS = (byte[])ss.Clone();
			header_length = length;
		}

		public void Decode(byte[] buffer)
		{
			for (int i = 0; i < header_length; i++)
			{
				d_pointer = d_pointer % 40;
				byte x = (byte)((buffer[i] - d_last_ch) ^ SS[d_pointer++]);
				d_last_ch = buffer[i];
				buffer[i] = x;
			}
		}

		public void Encode(byte[] buffer)
		{
			for (int i = 0; i < header_length; i++)
			{
				buffer[i] = (byte)(e_last_ch + (SS[e_pointer++] ^ buffer[i]));
				e_pointer = e_pointer % 40;
				e_last_ch = buffer[i];
			}
		}
	}

	public class RWProxyClient : AClient, IDisposable
	{

		private Socket s2c;
		private Socket c2s;

		private bool keyFound = false;
		private object locker = new object();
		private byte[] SS = new byte[40];

		private string s_name;
		private string k_name;
		private string c_name;

		private HeaderCoder c_coder;
		private HeaderCoder s_coder;

		public RWProxyClient(Socket s)
		{
			s2c = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
			c2s = s;
			string tmp_name = string.Format("raw/{0:yyyy-MM-dd-hh-mm-ss}.raw.", DateTime.Now);
			s_name = tmp_name + "s";
			k_name = tmp_name + "key";
			c_name = tmp_name + "c";
			Start();
		}

		public void ConsoleCommander()
		{
			byte[] buff = null;
			try
			{
				for (int i = 0; i < 0xffff; i++)
				{
					BinaryWriter writer;
					object locker;
					Console.WriteLine("QUEST QUERY: {0,6}", i);
					using (writer = new BinaryWriter(new MemoryStream()))
					{
						writer.Write((byte)0);
						writer.Write((byte)0x10);
						writer.Write((ushort)0x5c);
						writer.Write((ushort)0);
						writer.Write(i);
						writer.Write((ulong)0);
						buff = (writer.BaseStream as MemoryStream).ToArray();
					}
					lock ((locker = this.locker))
					{
						c_coder.Encode(buff);
						this.s2c.Send(buff, buff.Length, SocketFlags.None);
						Dump(buff, 0, buff.Length, this.c_name);
					}
					Thread.Sleep(50);
					Console.WriteLine("CREATURE QUERY: {0,6}", i);
					using (writer = new BinaryWriter(new MemoryStream()))
					{
						writer.Write((byte)0);
						writer.Write((byte)0x10);
						writer.Write((ushort)0x60);
						writer.Write((ushort)0);
						writer.Write(i);
						writer.Write((ulong)0);
						buff = (writer.BaseStream as MemoryStream).ToArray();
					}
					lock ((locker = this.locker))
					{
						c_coder.Encode(buff);
						this.s2c.Send(buff, buff.Length, SocketFlags.None);
						Dump(buff, 0, buff.Length, this.c_name);
					}
					Thread.Sleep(50);
					Console.WriteLine("PAGE TEXT QUERY: {0,6}", i);
					using (writer = new BinaryWriter(new MemoryStream()))
					{
						writer.Write((byte)0);
						writer.Write((byte)0x10);
						writer.Write((ushort)90);
						writer.Write((ushort)0);
						writer.Write(i);
						writer.Write((ulong)0);
						buff = (writer.BaseStream as MemoryStream).ToArray();
					}
					lock ((locker = this.locker))
					{
						c_coder.Encode(buff);
						this.s2c.Send(buff, buff.Length, SocketFlags.None);
						Dump(buff, 0, buff.Length, this.c_name);
					}
					Thread.Sleep(50);
				}
			}
			catch
			{
			}
		}

		private byte[] computeDigest(string name, uint client_seed, uint server_seed)
		{
			byte[] buff;
			using (BinaryWriter w = new BinaryWriter(new MemoryStream()))
			{
				w.Write(Encoding.UTF8.GetBytes(name));
				w.Write(0);
				w.Write(client_seed);
				w.Write(server_seed);
				w.Write(SS);
				w.Flush();
				buff = (w.BaseStream as MemoryStream).ToArray();
				buff = Utility.sha1.ComputeHash(buff, 0, buff.Length);
			}
			return buff;
		}

		private bool compareDigests(byte[] client_digest, byte[] server_digest)
		{
			for (int i = 0; i < 20; i++)
			{
				if (client_digest[i] != server_digest[i])
					return false;
			}
			return true;
		}

		private void Start()
		{
			byte[] buffer = new byte[0xffff];

			Console.WriteLine("client connected");

			//s2c.Connect("213.248.123.4", 3724);
			s2c.Connect("83.219.128.134", 8085);

			int recv = 0;
			int pointer;

			recv = s2c.Receive(buffer, 0, 65535, SocketFlags.None);
			pointer = 4;
			uint server_seed = Converter.ToUInt32(buffer, ref pointer);
			c2s.Send(buffer, 0, recv, SocketFlags.None);
			Dump(buffer, 0, recv, s_name);

			recv = c2s.Receive(buffer, 0, 65535, SocketFlags.None);//MUST BE CMSG_AUTH_SESSION
			pointer = 6 + 4 + 4;//header + uint32(client build) + uint32(unk)
			string accname = Converter.ToString0(buffer, ref pointer);
			uint client_seed = Converter.ToUInt32(buffer, ref pointer);
			byte[] client_digest = new byte[20];
			for (int i = 0; i < 20; i++)
				client_digest[i] = buffer[pointer++];
			s2c.Send(buffer, 0, recv, SocketFlags.None);
			Dump(buffer, 0, recv, c_name);

			Thread.CurrentThread.Priority = ThreadPriority.Highest;
			UWoW.Helper.Utility.ExtractKey(accname, SS);
			Thread.CurrentThread.Priority = ThreadPriority.Normal;

			byte[] server_digest = computeDigest(accname, client_seed, server_seed);

			Console.WriteLine("Check key... {0}", keyFound = compareDigests(client_digest, server_digest));
			if (keyFound)
			{
				using (Stream s = File.OpenWrite(k_name))
				{
					s.Write(SS, 0, 40);
				}

				new Thread(s2cThread).Start();
				new Thread(c2sThread).Start();
			}
			else
			{
				s2c.Close();
				c2s.Close();
			}
		}

		private bool __transfer(Socket socket_fr, Socket socket_to, HeaderCoder coder, byte[] buffer)
		{
			lock (coder)
			{
				int size;
				if ((size = socket_fr.Receive(buffer, 0, coder.HeaderLength, SocketFlags.None))
					> 0)
				{
					coder.Decode(buffer);//decoding header

					int length = (buffer[1] | (buffer[0] << 8)) + 2;
					int opcode = (buffer[2] | (buffer[3] << 8));

					coder.Encode(buffer);

					Console.WriteLine("{2}: {1,4} BYTES, OPCODE: {0} ", (OpCodes)opcode, length, (coder.HeaderLength == 4) ? "[S2C]" : "[C2S]");

					int n = 0;
					do
					{
						n = socket_fr.Receive(buffer, size, length - size, SocketFlags.None);
						if (n <= 0)
							break;// throw new SocketException();
						size += n;
					} while (size < length && size > 0);
					socket_to.Send(buffer, size, SocketFlags.None);
					Dump(buffer, 0, size, (coder.HeaderLength == 4) ? s_name : c_name);
				}
				else
				{
					throw new SocketException();
				}
				return true;
			}
		}

		private void s2cThread()
		{
			try
			{
				byte[] buffer = new byte[65536];
				s_coder = new HeaderCoder(SS, 4);
				while (true)
				{
					__transfer(s2c, c2s, s_coder, buffer);
				}
			}
			catch
			{
				Console.WriteLine("S2C Thread exited");
			}
		}

		private void c2sThread()
		{
			try
			{
				byte[] buffer = new byte[65536];
				c_coder = new HeaderCoder(SS, 6);
				while (true)
				{
					__transfer(c2s, s2c, c_coder, buffer);
				}
			}
			catch
			{
				Console.WriteLine("C2S Thread exited");
			}
		}

		public static void View(TextWriter tw, byte[] b, int offset, int len)
		{
			for (int i = 0; i < len; i += 0x10)
			{
				for (int j = i; (j < (i + 0x10)) && ((j + offset) < b.Length); j++)
				{
					if (j >= len)
					{
						tw.Write("   ");
					}
					else
					{
						tw.Write("{0:X2} ", b[j + offset]);
					}
				}
				for (int k = i; ((k + offset) < b.Length) && ((k < (i + 0x10)) && (k < len)); k++)
				{
					if (b[k + offset] < 0x20)
					{
						tw.Write(".");
					}
					else
					{
						tw.Write("{0:C}", (char)b[k + offset]);
					}
				}
				tw.WriteLine();
			}
		}

		private static void Dump(byte[] data, int offset, int length, string fname)
		{
			using (Stream s = File.Open(fname, FileMode.Append, FileAccess.Write))
			{
				s.Write(data, offset, length);
			}
		}

		#region IDisposable Members

		void IDisposable.Dispose()
		{
			throw new Exception("The method or operation is not implemented.");
		}

		#endregion
	}
}

