//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;

//namespace Hazzik {
//   [Flags]
//   public enum UpdataFieldFlags {
//      NONE = 0x00,
//      PUBLIC = 0x01,
//      PRIVATE = 0x02,
//      OWNER_ONLY = 0x04,
//      UNK1 = 0x08,
//      UNK2 = 0x10,
//      UNK3 = 0x20,
//      GROUP_ONLY = 0x40,
//      UNK4 = 0x80,
//      DYNAMIC = 0x100,
//   }

//   public enum UpdateFieldType {
//      NONE = 0,
//      INT = 1,
//      TWO_SHORT = 2,
//      FLOAT = 3,
//      LONG = 4,
//      BYTES = 5,
//   }

//   public class UpdateField {
//      public int Index { get; set; }
//      public int Length { get; set; }
//      public UpdateFieldType Type { get; set; }
//      public UpdataFieldFlags Flags { get; set; }

//      public UpdateField(int index, int length, int type, int flags) {
//         Index = index;
//         Length = length;
//         Type = (UpdateFieldType)type;
//         Flags = (UpdataFieldFlags)flags;
//      }
//   }

//   public static class UpdateFields1 {
//      public static readonly UpdateField OBJECT_FIELD_GUID = new UpdateField(0, 2, 4, 1);
//   }
//}
