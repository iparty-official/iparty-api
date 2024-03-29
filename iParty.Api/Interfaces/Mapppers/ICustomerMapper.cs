﻿using iParty.Api.Dtos.People;
using iParty.Api.Infra;
using iParty.Api.Views.People;
using iParty.Business.Models.People;
using System.Collections.Generic;

namespace iParty.Api.Interfaces.Mappers
{
    public interface ICustomerMapper
    {
        MapperResult<Person> Map(CustomerDto dto);

        CustomerView Map(Person person);

        List<CustomerView> Map(List<Person> people);
    }
}
