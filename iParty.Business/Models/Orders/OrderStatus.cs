using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iParty.Business.Models.Orders
{
    public enum OrderStatus
    {
        Draft = 0,
        SentToSupplier = 1,
        AcceptedBySupplier = 2,
        DeliveredToCustomer = 3,
        Finished = 4,
        Cancelled = 5
    }
}
