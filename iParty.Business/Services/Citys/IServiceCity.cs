using iParty.Business.Infra;
using iParty.Business.Models;
using iParty.Business.Models.Addresses;
using iParty.Business.Services.Citys.Dtos;
using System;

namespace iParty.Business.Services.Citys
{
    public interface IServiceCity : IService<City>
    {
        NewView Create(CityDto dto);
        NewView Update(Guid id, CityDto dto);        
    }
}
