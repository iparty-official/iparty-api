using AutoMapper;
using iParty.Api.Dtos;
using iParty.Api.Views;
using iParty.Business.Infra;
using iParty.Business.Models.Addresses;
using iParty.Business.Models.Messages;

namespace iParty.Api.AutoMapper
{
    public class AutoMapperConfiguration : Profile
    {
        public AutoMapperConfiguration()
        {
            CreateMap<City, CityView>().ReverseMap();
            CreateMap<City, CityDto>().ReverseMap();
            CreateMap<Message, MessageView>().ReverseMap();
            CreateMap<Message, MessageDto>().ReverseMap();
        }
    }
}
