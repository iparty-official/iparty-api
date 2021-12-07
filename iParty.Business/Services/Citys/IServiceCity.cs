using iParty.Business.Infra;
using iParty.Business.Services.Citys.Dtos;
using System;

namespace iParty.Business.Services.Citys
{
    public interface IServiceCity
    {
        NewView Create(CityDto dto);
        NewView Update(Guid id, CityDto dto);
        void Delete(Guid id);
    }
}
