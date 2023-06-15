﻿using System.Collections.Generic;

namespace ProductApi.Models
{
    public class OrderStatusChangedMessage
    {
        public int CustomerId { get; set; }
        public IList<OrderLine> OrderLines { get; set; }
    }
}
