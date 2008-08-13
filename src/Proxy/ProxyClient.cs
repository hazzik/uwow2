using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;
using System.IO;
using HelperTools;
using System.Threading;
using System.Collections;
using System.Net;

namespace UWoW
{
	public class RLProxyClient : AClient
	{
		private Socket s2c = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
		private Socket c2s = null;

		public RLProxyClient(Socket s, string forward_host, int forward_port)
		{
			c2s = s;
			Start();
		}
		public RLProxyClient(Socket s, EndPoint forwardEndPoint)
		{
			c2s = s;
			Start();
		}

		private void Start()
		{
			byte[] buff = new byte[0xFFFF];

			ArrayList tmp = new ArrayList(new Socket[] { s2c, c2s });
			ArrayList checkRead = new ArrayList();

			//s2c.Connect("eu.logon.worldofwarcraft.com", 3724);
			s2c.Connect("83.219.128.134", 3724);

			int n = buff.Length;
			TextWriter tw = new StreamWriter("RL-Proxy-" + DateTime.Now.ToString("HH-mm-ss") + ".log");
			try
			{
				do
				{
					checkRead = (ArrayList)tmp.Clone();
					Socket.Select(checkRead, null, null, -1);
					foreach (Socket s in checkRead)
					{
						n = s.Receive(buff, 0xFFFF, SocketFlags.None);
						tw.WriteLine();
						if (s == s2c)// from server
						{
							tw.WriteLine("<<S<< {0} bytes", n);
							View(tw, buff, 0, n);

							if (buff[0] == 16) // REALMLIST
							{
								int len = (buff[1]) | (buff[2] << 8) + 3;
								while (n < len)
								{
									n += s.Receive(buff, n, len - n, SocketFlags.None);
								}
								using (Stream stream = new MemoryStream(buff, 3, len - 3))
								using (BinaryReader r = new BinaryReader(stream))
								{
									uint unk1 = r.ReadUInt32();//allways 0
									int count = r.ReadUInt16();//num realms
									for (int i = 0; i < count; i++)
									{
										byte byte1 = r.ReadByte();
										byte byte2 = r.ReadByte();
										byte byte3 = r.ReadByte();

										string name = UWoW.Helper.Utility.ReadSz(stream);
										string addr = UWoW.Helper.Utility.ReadSz(stream);

										float popul = r.ReadSingle();

										byte byte4 = r.ReadByte();
										byte byte5 = r.ReadByte();
										byte byte6 = r.ReadByte();
										Console.WriteLine("{0} {1} {2} {3} {4} {5} {6} {7} {8}",
											byte1, byte2, byte3, name, addr, popul, byte4, byte5, byte6
											);
									}
								}
								c2s.Send(FakeRL, FakeRL.Length, SocketFlags.None);
							}
							else
								c2s.Send(buff, n, SocketFlags.None);
						}
						else
						{
							tw.WriteLine(">>C>> {0} bytes", n);
							View(tw, buff, 0, n);
							s2c.Send(buff, n, SocketFlags.None);
						}
					}
				} while (n > 0);
			}
			catch { }
			s2c.Close();
			c2s.Close();
			tw.Flush();
			tw.Close();
		}

		private byte[] fakeRL;
		public byte[] FakeRL
		{
			get
			{
				if (fakeRL == null)
				{
					MemoryStream ms = new MemoryStream();
					IGenWriter gw = new GenericWriter(ms);
					gw.Write((byte)16);
					gw.Write((ushort)0); // packet size
					gw.Write(0);
					gw.Write((ushort)1); // num realms

					/*
					 * 0 - Normal
					 * 1 - PVP
					 * 6 - RP
					 * 8 - RPPVP
					 */
					gw.Write((byte)0);

					/*
					 * 1 - locked
					 */
					gw.Write((byte)0);

					/* 
					 * Status BitMask
					 * 128 - full
					 * 64 - recomended
					 * 2 - offline
					 * 1 - red
					 */
					gw.Write((byte)0);

					gw.WriteString0("W0W-proxy");
					gw.WriteString0("localhost:3726");
					gw.Write((float)1);
					gw.Write((byte)1);
					gw.Write((byte)1);
					gw.Write((byte)0x0F);

					gw.Write((ushort)5);

					gw.BaseStream.Seek(1, SeekOrigin.Begin);
					gw.Write((ushort)(gw.BaseStream.Length - 3));
					gw.Close();

					fakeRL = ms.ToArray();

				}
				return fakeRL;
			}
		}

		public static void View(TextWriter tw, byte[] b, int offset, int len)
		{
			int i1;
			int i2;
			int i3;
			for (i1 = 0; (i1 < len); i1 += 16)
			{
				tw.Write("{0:X10}: ", i1);
				for (i2 = i1; ((i2 < (i1 + 16)) && ((i2 + offset) < b.Length)); i2++)
				{
					if (i2 >= len)
						tw.Write("-- ");
					else
						tw.Write("{0:X2} ", b[i2 + offset]);
					if (i2 == i1 + 8)
						tw.Write("| ");
				}
				tw.Write(" ");
				for (i3 = i1; (((i3 + offset) < b.Length) && ((i3 < (i1 + 16)) && (i3 < len))); i3++)
					if (b[i3 + offset] < 32 || b[i3 + offset] > 127)
						tw.Write(".");
					else
						tw.Write("{0}", ((char)(b[i3 + offset])).ToString());

				tw.WriteLine();
			}
		}
	}
}
