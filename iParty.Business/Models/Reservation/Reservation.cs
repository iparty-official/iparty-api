using iParty.Business.Models.Items;
using System;

namespace iParty.Business.Models.Reservation
{
    public class Reservation: Entity
    {
        public Reservation() : base() { }
        public Reservation(DateTime date, int initialHour, int finalHour, Item item, OrderItemForReservation orderItem, ReservationReason reservationReason)
        {
            Date = date;
            InitialHour = initialHour;
            FinalHour = finalHour;
            Item = item;
            OrderItem = orderItem;
            ReservationReason = reservationReason;
        }
        public DateTime Date { get; private set; }
        public int InitialHour { get; private set; }
        public int FinalHour { get; private set; }
        public Item Item { get; private set; }
        public OrderItemForReservation OrderItem { get; private set; }
        public ReservationReason ReservationReason { get; private set; }
    }
}
