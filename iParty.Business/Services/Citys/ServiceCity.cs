using iParty.Business.Infra;
using iParty.Business.Models.Addresses;
using iParty.Business.Services.Citys.Dtos;
using iParty.Data.Repositories;
using System;

namespace iParty.Business.Services.Citys
{
    public class ServiceCity : IServiceCity
    {
        private readonly IRepository<City> _rep;
        public ServiceCity(IRepository<City> rep)
        {
            _rep = rep;
        }

        public NewView Create(CityDto dto) 
        {
            var city = City.Create(dto.Name, dto.IbgeNumber);
            _rep.Create(city);
            return new NewView(city.Id);
        }

        public NewView Update(Guid id, CityDto dto)
        {
            var city = _rep.RecoverById(id);
            if (city == null)
                return null; //Verificar como tratar erros 
            city.Update(dto.Name, dto.IbgeNumber);
            _rep.Update(city);
            return new NewView(city.Id);
        }

        public void Delete(Guid id)
        {            
            _rep.Delete(id);
        }
    }
}
