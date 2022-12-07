using System;
using System.Collections.Generic;

namespace AutoReapairShop.DbContex.Models
{
    public partial class Record
    {
        public Record()
        {
            Schedules = new HashSet<Schedule>();
        }

        public long RecordId { get; set; }
        public string Title { get; set; } = null!;
        public string AutoMark { get; set; } = null!;
        public string? Description { get; set; }

        public virtual ICollection<Schedule> Schedules { get; set; }
    }
}
