using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace Hazzik.Data.Xml {
	public class XmlDao<T> : IDao<T> {
		#region Fields

		private FileInfo _file = new FileInfo(@"..\..\..\" + typeof(T).Name + @".xml");
		private XmlSerializer _serializer = new XmlSerializer(typeof(List<T>));
		protected List<T> _entities = new List<T>();

		#endregion

		#region ctors

		protected XmlDao() {
			if(_file.Exists) {
				using(var s = _file.Open(FileMode.Open, FileAccess.Read)) {
					_entities = (List<T>)_serializer.Deserialize(s);
				}
			}
		}

		#endregion

		#region IDao<T> Members

		public void Delete(T entity) {
			_entities.Remove(entity);
		}

		public void Save(T entity) {
			if(!_entities.Contains(entity)) {
				_entities.Add(entity);
			}
		}

		public void SubmitChanges() {
			using(var s = _file.Open(FileMode.Create, FileAccess.Write)) {
				_serializer.Serialize(s, _entities);
			}
		}

		#endregion
	}
}
