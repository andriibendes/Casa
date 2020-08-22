using System;
using System.Collections.Generic;

namespace Casa
{
    public partial class Check
    {
        public Check()
        {
            CheckProduct = new HashSet<CheckProduct>();
        }

        public int Id { get; set; }
        public DateTime Date { get; set; }

        public virtual ICollection<CheckProduct> CheckProduct { get; set; }
    }
}
