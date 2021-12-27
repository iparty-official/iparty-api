using iParty.Business.Models.Items;
using iParty.Business.Models.Orders;
using System;

namespace iParty.Business.Models.Reservation
{
    public class Reservation: Entity
    {
        public DateTime Date { get; set; }
        public int InitialHour { get; set; }
        public int FinalHour { get; set; }
        public Item Item { get; set; }
        public Order Order { get; set; }
        public ReservationReason ReservationReason { get; set; }
    }
}
