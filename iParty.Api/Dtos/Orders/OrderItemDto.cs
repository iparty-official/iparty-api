﻿using iParty.Business.Models.Items;
using System;

namespace iParty.Api.Dtos.Orders
{
    public class OrderItemDto
    {
        public Guid ItemId { get; set; }
        public int Quantity { get; set; }        
    }
}
