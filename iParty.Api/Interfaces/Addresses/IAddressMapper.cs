using iParty.Api.Dtos.Addresses;
using iParty.Api.Infra;
using iParty.Business.Models.Addresses;

namespace iParty.Api.Interfaces.Addresses
{
    public interface IAddressMapper
    {
        public MapperResult<Address> Map(AddressDto dto);
    }
}
