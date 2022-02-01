using iParty.Api.Dtos.Addresses;
using iParty.Api.Infra;
using iParty.Api.Interfaces.Mappers;
using iParty.Business.Infra.Extensions;
using iParty.Business.Models.Addresses;
using iParty.Business.Interfaces;
using System.Collections.Generic;

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

            SetEntity(new Address(dto.ZipCode, dto.Street, dto.Number, dto.District, city));

            return GetResult();
        }

        public List<MapperResult<Address>> Map(List<AddressDto> dtos)
        {           
            var result = new List<MapperResult<Address>>();

            foreach (var dto in dtos)
            {
                //this.ClearResult(); TODO: Rever
                result.Add(this.Map(dto));
            }

            return result;
        }
    }
}
