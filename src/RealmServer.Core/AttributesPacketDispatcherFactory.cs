using System;
using System.Collections.Generic;
using System.Reflection;
using Hazzik.Attributes;

namespace Hazzik {
	public class AttributesPacketDispatcherFactory : IPacketDispatcherFactory {
		private readonly IList<Assembly> _assemblies = new List<Assembly>();
		private readonly IDictionary<int, IPacketDispatcher> _dispatchers = new Dictionary<int, IPacketDispatcher>();

		#region IPacketDispatcherFactory Members

		public IPacketDispatcher GetDispatcher(WMSG wmsg) {
			IPacketDispatcher dispatcher;
			_dispatchers.TryGetValue((int)wmsg, out dispatcher);
			return dispatcher;
		}

		#endregion

		public void AddAssembly(Assembly assembly) {
			_assemblies.Add(assembly);
		}

		public void Load() {
			foreach(Assembly assembly in _assemblies) {
				foreach(Type type in assembly.GetTypes()) {
					LoadType(type);
				}
			}
		}

		protected void LoadType(Type type) {
			var attribs = (PacketHandlerClassAttribute[])type.GetCustomAttributes(typeof(PacketHandlerClassAttribute), true);
			if(attribs.Length == 0) {
				return;
			}
			foreach(PacketHandlerClassAttribute attrib in attribs) {
				_dispatchers[(int)attrib.MessageType] = (IPacketDispatcher)Activator.CreateInstance(type);
			}
		}
	}
}