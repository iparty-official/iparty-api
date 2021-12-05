using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iParty.Business.Models.People
{
    public class Person: Entity
    {
        public object User { get; set; }
        public string Name { get; set; }
        public string Document { get; set; }
        public object Photo { get; set; }      
        public SupplierOrCustomer SupplierOrCustomer { get; set; }
        public Customer CustomerInfo { get; set; }
        public Supplier SupplierInfo { get; set; }

    }
}
