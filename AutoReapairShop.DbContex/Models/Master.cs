using System;
using System.Collections.Generic;

namespace AutoReapairShop.DbContex.Models
{
    public partial class Master
    {
        public Master()
        {
            Schedules = new HashSet<Schedule>();
        }

        public long MasterId { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string? Patronymic { get; set; }
        public string Phone { get; set; } = null!;
        public string Email { get; set; } = null!;

        public virtual ICollection<Schedule> Schedules { get; set; }
    }
}
