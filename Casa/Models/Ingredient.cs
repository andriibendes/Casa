using System;
using System.Collections.Generic;

namespace Casa
{
    public partial class Ingredient
    {
        public Ingredient()
        {
            ProductIngredient = new HashSet<ProductIngredient>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }

        public virtual ICollection<ProductIngredient> ProductIngredient { get; set; }
    }
}
