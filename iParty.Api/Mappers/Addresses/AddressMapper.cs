using iParty.Api.Dtos.Addresses;
using iParty.Api.Interfaces.Addresses;
using iParty.Business.Models.Addresses;
using iParty.Data.Repositories;
using System;

namespace iParty.Api.Mappers.Addresses
{
    public class AddressMapper : IAddressMapper
    {
        private IRepository<City> _cityRepository;

        public AddressMapper(IRepository<City> cityRepository)
        {
            _cityRepository = cityRepository;
        }

        private City getCity(Guid id, string notFoundMessage)
        {
            var city = _cityRepository.RecoverById(id);

            if (city == null)
            {
                throw new Exception(notFoundMessage);
            }

            return city;
        }

        public Address Map(AddressDto dto)
        {
            return new Address()
            {
                ZipCode = dto.ZipCode,
                Street = dto.Street,
                District = dto.District,
                City = getCity(dto.CityId, "A cidade do endereço não foi encontrada")
            };
        }
    }
}
