using System;
using System.Collections.Generic;

namespace Casa
{
    public partial class Product
    {
        public Product()
        {
            CheckProduct = new HashSet<CheckProduct>();
            ProductIngredient = new HashSet<ProductIngredient>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public bool Piece { get; set; }
        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }
        public virtual ICollection<CheckProduct> CheckProduct { get; set; }
        public virtual ICollection<ProductIngredient> ProductIngredient { get; set; }
    }
}
