using iParty.Api.Dtos;
using iParty.Api.Views;
using iParty.Business.Models.Messages;
using iParty.Business.Models.People;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace iParty.Api.Interfaces
{
    public interface ICustomerMapper
    {
        Person Map(CustomerDto dto);

        CustomerView Map(Person dto);

        List<CustomerView> Map(List<Person> people);
    }
}
