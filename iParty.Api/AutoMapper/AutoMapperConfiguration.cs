using AutoMapper;
using iParty.Api.Dtos;
using iParty.Api.Dtos.Addresses;
using iParty.Api.Dtos.Messages;
using iParty.Api.Dtos.Notifications;
using iParty.Api.Dtos.People;
using iParty.Api.Views.Addresses;
using iParty.Api.Views.Messages;
using iParty.Api.Views.Notifications;
using iParty.Api.Views.PaymentPlans;
using iParty.Api.Views.People;
using iParty.Business.Models.Addresses;
using iParty.Business.Models.Messages;
using iParty.Business.Models.Notications;
using iParty.Business.Models.PaymentPlans;
using iParty.Business.Models.People;

namespace iParty.Api.AutoMapper
{
    public class AutoMapperConfiguration : Profile
    {
        public AutoMapperConfiguration()
        {
            CreateMap<City, CityDto>().ReverseMap();
            CreateMap<City, CityView>().ReverseMap();            
            
            CreateMap<Person, PersonDto>().ReverseMap();
            CreateMap<Person, PersonView>().ReverseMap();
            
            CreateMap<Person, CustomerDto>().ReverseMap();
            CreateMap<Person, CustomerView>().ReverseMap();
            
            CreateMap<Person, PersonSummarizedView>().ReverseMap();

            CreateMap<Message, MessageDto>().ReverseMap();
            CreateMap<Message, MessageView>().ReverseMap();

            CreateMap<Notification, NotificationDto>().ReverseMap();
            CreateMap<Notification, NotificationView>().ReverseMap();
            
            CreateMap<Phone, PhoneDto>().ReverseMap();
            CreateMap<Phone, PhoneView>().ReverseMap();
            
            CreateMap<Address, AddressDto>().ReverseMap();
            CreateMap<Address, AddressView>().ReverseMap();
            
            CreateMap<PaymentPlan, PaymentPlanDto>().ReverseMap();            
            CreateMap<PaymentPlan, PaymentPlanView>().ReverseMap();

            CreateMap<PaymentPlanInstalment, PaymentPlanInstalmentDto>().ReverseMap();
            CreateMap<PaymentPlanInstalment, PaymentPlanInstalmentView>().ReverseMap();            
        }
    }
}
