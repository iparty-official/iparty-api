using iParty.Api.Dtos.Addresses;
using iParty.Api.Infra;
using iParty.Api.Interfaces.Mappers;
using iParty.Business.Infra.Extensions;
using iParty.Business.Models.Addresses;
using iParty.Business.Interfaces;
using System.Collections.Generic;
using iParty.Business.Models.Cities;
using iParty.Api.Views.Addresses;
using System;

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

            SetEntity(new Address(dto.ZipCode, dto.Street, dto.Number, dto.District, new CityForAddress(city.Id, city.Name, city.State)));

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

        public AddressView Map(Address domain)
        {
            if (domain == null) return null;

            var view = new AddressView
            {
                ZipCode = domain.ZipCode, 
                Street = domain.Street,
                District = domain.District,
                Number = domain.Number,
                CityId = domain.City.Id
            };

            return view;
        }

        public List<AddressView> Map(List<Address> domains)
        {
            throw new System.NotImplementedException();
        }
    }
}
