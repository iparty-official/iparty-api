using iParty.Api.Dtos.Addresses;
using iParty.Api.Infra;
using iParty.Api.Interfaces.Mappers;
using iParty.Business.Infra.Extensions;
using iParty.Business.Models.Addresses;
using iParty.Data.Repositories;

namespace iParty.Api.Mappers.Addresses
{
    public class AddressMapper : BaseMapper<Address>, IAddressMapper
    {
        private IRepository<City> _cityRepository;

        public AddressMapper(IRepository<City> cityRepository)
        {
            _cityRepository = cityRepository;
        }        

        public MapperResult<Address> Map(AddressDto dto)
        {
            var city = _cityRepository.RecoverById(dto.CityId).IfNull(() => AddError("A cidade do endereço não foi encontrada"));

            if (!SuccessResult()) return GetResult();

            SetEntity(new Address()
            {
                ZipCode = dto.ZipCode,
                Street = dto.Street,
                Number = dto.Number,
                District = dto.District,
                City = city
            });

            return GetResult();
        }        
    }
}
