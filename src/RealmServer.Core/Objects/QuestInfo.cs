using System;

namespace Hazzik.Objects {
    public class QuestInfo {
        public static readonly QuestInfo Empty = new QuestInfo();
        
        public int Id { get; set; }

        public uint FinishTime { get; set; }

        public short Short1 { get; set; }

        public short Short2 { get; set; }

        public short Short3 { get; set; }

        public short Short4 { get; set; }
    }
}