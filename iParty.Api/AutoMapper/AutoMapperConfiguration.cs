using AutoMapper;
using iParty.Api.Dtos;
using iParty.Api.Views;
using iParty.Business.Models.Addresses;
using iParty.Business.Models.Messages;
using iParty.Business.Models.Notications;
using iParty.Business.Models.People;

namespace iParty.Api.AutoMapper
{
    public class AutoMapperConfiguration : Profile
    {
        public AutoMapperConfiguration()
        {
            CreateMap<City, CityView>().ReverseMap();
            CreateMap<City, CityDto>().ReverseMap();
            CreateMap<Person, PersonDto>().ReverseMap();
            CreateMap<Person, CustomerDto>().ReverseMap();
            CreateMap<Person, CustomerView>().ReverseMap();
            CreateMap<Person, PersonView>().ReverseMap();
            CreateMap<Person, PersonSummarizedView>().ReverseMap();            
            CreateMap<Message, MessageView>().ReverseMap();            
            CreateMap<Notification, NotificationView>().ReverseMap();
            CreateMap<Phone, PhoneDto>().ReverseMap();
            CreateMap<Phone, PhoneView>().ReverseMap();
            CreateMap<Address, AddressView>().ReverseMap();
        }
    }
}
