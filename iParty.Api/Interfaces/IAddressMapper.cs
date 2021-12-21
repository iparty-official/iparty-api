using iParty.Api.Dtos;
using iParty.Business.Models.Addresses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace iParty.Api.Interfaces
{
    public interface IAddressMapper
    {
        Address Map(AddressDto dto);
    }
}
