using iParty.Business.Models.Items;
using System;

namespace iParty.Business.Models.Reservation
{
    public class Reservation: Entity
    {
        public DateTime Date { get; set; }
        public int InitialHour { get; set; }
        public int FinalHour { get; set; }
        public Item Item { get; set; }
        public OrderItemForReservation OrderItem { get; set; }
        public ReservationReason ReservationReason { get; set; }
    }
}
