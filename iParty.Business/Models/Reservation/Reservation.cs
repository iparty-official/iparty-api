using iParty.Business.Models.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iParty.Business.Models.Reservation
{
    public class Reservation: Entity
    {
        public DateTime Date { get; set; }
        public int InitialHour { get; set; }
        public int FinalHour { get; set; }
        public OrderItem OrderItem { get; set; }
        public ReservationReason ReservationReason { get; set; }
    }
}
