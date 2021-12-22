using iParty.Api.Dtos.People;
using iParty.Api.Infra;
using iParty.Api.Views.People;
using iParty.Business.Models.People;
using System.Collections.Generic;

namespace iParty.Api.Interfaces.People
{
    public interface ICustomerMapper
    {
        MapperResult<Person> Map(CustomerDto dto);

        CustomerView Map(Person dto);

        List<CustomerView> Map(List<Person> people);
    }
}
