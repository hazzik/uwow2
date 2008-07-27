using System;
using System.Collections.Generic;
using System.Text;

namespace UWoW
{
	public class WorldObject
	{
		/*
 		OBJECT_FIELD_GUID = 0, // 2 4 1
		OBJECT_FIELD_TYPE = 2, // 1 1 1
		OBJECT_FIELD_ENTRY = 3, // 1 1 1
		OBJECT_FIELD_SCALE_X = 4, // 1 3 1
		OBJECT_FIELD_PADDING = 5, // 1 1 0
		 */

		#region ctors



		#endregion // ctors

		#region fields

		private ulong	_guid;
		private int		_type;
		private int		_entry; //id
		private float	_scale_x;
		private int		_padding; // wtf?

		#endregion // fields

		#region accessors

		public virtual ulong Guid
		{
			get { return _guid; }
			set { _guid = value; }
		}

		#endregion //accessors

	}
}
