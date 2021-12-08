using AutoMapper;
using iParty.Api.View;
using iParty.Business.Models.Addresses;

namespace iParty.Api.AutoMapper
{
    public class AutoMapperConfiguration : Profile
    {
        public AutoMapperConfiguration()
        {
            CreateMap<City, CityView>().ReverseMap();            
        }
    }
}
