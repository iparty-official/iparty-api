using iParty.Api.Dtos.Addresses;
using iParty.Business.Models.Addresses;

namespace iParty.Api.Interfaces.Addresses
{
    public interface IAddressMapper
    {
        Address Map(AddressDto dto);
    }
}
