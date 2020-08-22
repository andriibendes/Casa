using System;
using System.Collections.Generic;

namespace Casa
{
    public partial class CheckProduct
    {
        public int Id { get; set; }
        public int CheckId { get; set; }
        public int ProductId { get; set; }
        public double Quantity { get; set; }

        public virtual Check Check { get; set; }
        public virtual Product Product { get; set; }
    }
}
