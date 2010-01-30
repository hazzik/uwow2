using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Hazzik.Net;

namespace Hazzik.RealmServer.PacketDispatchers {
    public class AttributesPacketDispatcherFactory : IPacketDispatcherFactory {
        private readonly IList<Assembly> assemblies = new List<Assembly>();
        private readonly IDictionary<int, IPacketDispatcher> dispatchers = new Dictionary<int, IPacketDispatcher>();

        #region IPacketDispatcherFactory Members

        public IPacketDispatcher GetDispatcher(WMSG wmsg) {
            IPacketDispatcher dispatcher;
            dispatchers.TryGetValue((int)wmsg, out dispatcher);
            return dispatcher;
        }

        #endregion

        public void AddAssembly(Assembly assembly) {
            assemblies.Add(assembly);
        }

        public void Load() {
            foreach(Type type in assemblies.SelectMany(assembly => assembly.GetTypes())) {
                LoadType(type);
            }
        }

        private void LoadType(Type type) {
            WorldPacketHandlerAttribute[] attribs = type.GetCustomAttributes<WorldPacketHandlerAttribute>(true);
            if(attribs.Length == 0) {
                return;
            }
            foreach(WorldPacketHandlerAttribute attrib in attribs) {
                dispatchers[(int)attrib.MessageType] = (IPacketDispatcher)Activator.CreateInstance(type);
            }
        }
    }
}