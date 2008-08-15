using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;
using System.IO;
//using HelperTools;
using System.Threading;
using System.Collections;
using System.Net;
using Hazzik.Helper;
using Hazzik;

namespace UWoW {
	public class AuthProxy : ClientBase {
		private Socket s2c = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
		private Socket c2s = null;

		public AuthProxy(Socket s, string forward_host, int forward_port) {
			c2s = s;
			s2c.Connect(forward_host, forward_port);
			Start();
		}

		public AuthProxy(Socket s, EndPoint forwardEndPoint) {
			c2s = s;
			s2c.Connect(forwardEndPoint);
			Start();
		}

		private void Start() {
			byte[] buff = new byte[0xFFFF];

			ArrayList tmp = new ArrayList(new Socket[] { s2c, c2s });
			ArrayList checkRead = new ArrayList();

			int n = buff.Length;
			TextWriter tw = new StreamWriter("RL-Proxy-" + DateTime.Now.ToString("HH-mm-ss") + ".log");
			try {
				do {
					checkRead = (ArrayList)tmp.Clone();
					Socket.Select(checkRead, null, null, -1);
					foreach(Socket s in checkRead) {
						n = s.Receive(buff, 0xFFFF, SocketFlags.None);
						tw.WriteLine();
						if(s == s2c)// from server
						{
							tw.WriteLine("<<S<< {0} bytes", n);
							Utility.View(tw, buff, 0, n);

							if(buff[0] == 16) // REALMLIST
							{
								int len = (buff[1]) | (buff[2] << 8) + 3;
								while(n < len) {
									n += s.Receive(buff, n, len - n, SocketFlags.None);
								}
								using(Stream stream = new MemoryStream(buff, 3, len - 3))
								using(BinaryReader r = new BinaryReader(stream)) {
									uint unk1 = r.ReadUInt32();//allways 0
									int count = r.ReadUInt16();//num realms
									for(int i = 0; i < count; i++) {
										byte byte1 = r.ReadByte();
										byte byte2 = r.ReadByte();
										byte byte3 = r.ReadByte();

										string name = r.ReadCString();
										string addr = r.ReadCString();

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
						else {
							tw.WriteLine(">>C>> {0} bytes", n);
							Utility.View(tw, buff, 0, n);
							s2c.Send(buff, n, SocketFlags.None);
						}
					}
				} while(n > 0);
			} catch { }
			s2c.Close();
			c2s.Close();
			tw.Flush();
			tw.Close();
		}

		private byte[] fakeRL;
		public byte[] FakeRL {
			get {
				if(fakeRL == null) {
					using(var ms = new MemoryStream()) {
						BinaryWriter w = new BinaryWriter(ms);
						w.Write((byte)16);
						w.Write((ushort)0); // packet size
						w.Write(0);
						w.Write((ushort)1); // num realms

						/*
						 * 0 - Normal
						 * 1 - PVP
						 * 6 - RP
						 * 8 - RPPVP
						 */
						w.Write((byte)0);

						/*
						 * 1 - locked
						 */
						w.Write((byte)0);

						/* 
						 * Status BitMask
						 * 128 - full
						 * 64 - recomended
						 * 2 - offline
						 * 1 - red
						 */
						w.Write((byte)0);

						w.WriteCString("W0W-proxy");
						w.WriteCString("localhost:3726");
						w.Write((float)1);
						w.Write((byte)1);
						w.Write((byte)1);
						w.Write((byte)0x0F);

						w.Write((ushort)5);

						w.BaseStream.Seek(1, SeekOrigin.Begin);
						w.Write((ushort)(w.BaseStream.Length - 3));
						w.Close();

						fakeRL = (w.BaseStream as MemoryStream).ToArray();
					}
				}
				return fakeRL;
			}
		}
	}
}
