using iParty.Business.Models.Addresses;
using System.Collections.Generic;

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
        
        public List<Address> Addresses { get; set; }

        public List<Phone> Phones { get; set; }
    }
}
