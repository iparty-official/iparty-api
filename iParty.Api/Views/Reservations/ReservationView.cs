﻿using iParty.Api.Views.Items;
using iParty.Api.Views.Orders;
using iParty.Business;
using System;

namespace iParty.Api.Views.Reservations
{
    public class ReservationView : View
    {
        public DateTime Date { get; set; }
        public int InitialHour { get; set; }
        public int FinalHour { get; set; }        
        public ItemSummarizedView Item { get; set; }
        public OrderView Order { get; set; }
        public ReservationReason ReservationReason { get; set; }
    }
}
