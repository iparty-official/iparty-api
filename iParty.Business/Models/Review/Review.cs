using iParty.Business.Models.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iParty.Business.Models.Review
{
    public class Review: Entity
    {
        public DateTime Date { get; set; }
        public DateTime Time { get; set; }
        public int Stars { get; set; }
        public string Description { get; set; }
        public OrderItem OrderItem { get; set; }
    }
}
