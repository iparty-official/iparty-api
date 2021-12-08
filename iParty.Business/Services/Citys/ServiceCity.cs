using iParty.Business.Infra;
using iParty.Business.Models.Addresses;
using iParty.Business.Services.Citys.Dtos;
using iParty.Business.Validations;
using iParty.Data.Repositories;
using System;

namespace iParty.Business.Services.Citys
{
    public class ServiceCity : Service<City, IRepository<City>>, IServiceCity
    {
        public ServiceCity(IRepository<City> rep) : base(rep)
        {
        }

        public NewView Create(CityDto dto) 
        {            
            var city = City.Create(dto.Name, dto.IbgeNumber);

            var result = ExecuteValidation(new CityValidation(), city);

            if (!result.IsValid)
                return new NewView(result);

            Rep.Create(city);

            return new NewView(city.Id);
        }

        public NewView Update(Guid id, CityDto dto)
        {
            var city = Rep.RecoverById(id);
            if (city == null)
                return null; //Verificar como tratar erros 
            city.Update(dto.Name, dto.IbgeNumber);
            Rep.Update(city);
            return new NewView(city.Id);
        }      
    }
}
