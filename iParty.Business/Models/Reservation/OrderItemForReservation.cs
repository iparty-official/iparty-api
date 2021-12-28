using System;

namespace iParty.Business.Models.Reservation
{
    public class OrderItemForReservation
    {
        public Guid OrderId { get; set; }

        public Guid OrderItemId { get; set; }
    }
}
