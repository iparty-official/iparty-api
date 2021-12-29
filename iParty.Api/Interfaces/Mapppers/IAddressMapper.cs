using iParty.Api.Dtos.Addresses;
using iParty.Api.Infra;
using iParty.Business.Models.Addresses;
using System.Collections.Generic;

namespace iParty.Api.Interfaces.Mappers
{
    public interface IAddressMapper
    {
        public MapperResult<Address> Map(AddressDto dto);

        public List<MapperResult<Address>> Map(List<AddressDto> dtos);
    }
}
