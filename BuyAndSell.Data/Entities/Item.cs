﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuySell.Data.Entities
{
    public class Item : Base
    {
        public string Name { get; set; } = default!;
        public int Price { get; set; }
        public int Ammount { get; set; }
        public string Description { get; set; } = default!;
        public string ImageUrl { get; set; } = default!;
        public virtual List<OrderItem> OrderItems { get; set; } = default!;

    }
}
