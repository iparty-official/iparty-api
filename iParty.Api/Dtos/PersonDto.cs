using iParty.Business.Models.Addresses;
using iParty.Business.Models.People;
using System.Collections.Generic;

namespace iParty.Api.Dtos
{
    public class PersonDto
    {
        public object User { get; set; }

        public string Name { get; set; }

        public string Document { get; set; }

        public object Photo { get; set; }

        public SupplierOrCustomer SupplierOrCustomer { get; set; }        

        public List<Address> Addresses { get; set; }

        public List<Phone> Phones { get; set; }
    }
}
