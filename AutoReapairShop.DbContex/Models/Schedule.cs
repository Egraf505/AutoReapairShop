using System;
using System.Collections.Generic;

namespace AutoReapairShop.DbContex.Models
{
    public partial class Schedule
    {
        public long ScheduleId { get; set; }
        public DateOnly DateOfWeek { get; set; }
        public TimeOnly TimeOfWeek { get; set; }
        public long FkMasterId { get; set; }
        public long FkRecordId { get; set; }

        public virtual Master FkMaster { get; set; } = null!;
        public virtual Record? FkRecord { get; set; } = null!;
    }
}
