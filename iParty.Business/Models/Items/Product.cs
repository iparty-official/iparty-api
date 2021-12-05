using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iParty.Business.Models.Items
{
    public class Product
    {
        public int AvailableQuantity { get; set; }
        public RentOrSale ForRentOrSale { get; set; }
    }
}
