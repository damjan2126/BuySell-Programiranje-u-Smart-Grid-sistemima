﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuySell.Contracts.DTOs.Item
{
    public class ItemViewDto
    {
        public long Id { get; set; }
        public string Name { get; set; } = default!;
        public int Price { get; set; }
        public int Ammount { get; set; }
        public string Description { get; set; } = default!;
        public string ImageUrl { get; set; } = default!;
        public string DeliveryFee { get; set; } = default!;
        public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAtUtc { get; set; } = DateTime.UtcNow;
        public long CreatedByUserId { get; set; }
        public long UpdatedByUserId { get; set; }
    }
}
