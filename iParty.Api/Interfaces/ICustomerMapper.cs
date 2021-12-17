using iParty.Api.Dtos;
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
    }
}
