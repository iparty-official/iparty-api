using iParty.Business.Models.People;
using System;

namespace iParty.Business.Models.Items
{
    public class PersonForItem
    {
        public PersonForItem()
        {
        }
        public PersonForItem(Guid id, string name, SupplierOrCustomer supplierOrCustomer)
        {
            Id = id;
            Name = name;
            SupplierOrCustomer = supplierOrCustomer;
        }
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public SupplierOrCustomer SupplierOrCustomer { get; private set; }
    }
}