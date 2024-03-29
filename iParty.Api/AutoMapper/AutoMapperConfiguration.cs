﻿using AutoMapper;
using iParty.Api.Dtos.Addresses;
using iParty.Api.Dtos.Cities;
using iParty.Api.Dtos.Items;
using iParty.Api.Dtos.Messages;
using iParty.Api.Dtos.Notifications;
using iParty.Api.Dtos.PaymentPlans;
using iParty.Api.Dtos.People;
using iParty.Api.Dtos.Reviews;
using iParty.Api.Dtos.Users;
using iParty.Api.Views.Addresses;
using iParty.Api.Views.Cities;
using iParty.Api.Views.Items;
using iParty.Api.Views.Messages;
using iParty.Api.Views.Notifications;
using iParty.Api.Views.Orders;
using iParty.Api.Views.PaymentPlans;
using iParty.Api.Views.People;
using iParty.Api.Views.Reviews;
using iParty.Api.Views.Users;
using iParty.Business.Models.Addresses;
using iParty.Business.Models.Cities;
using iParty.Business.Models.Items;
using iParty.Business.Models.Messages;
using iParty.Business.Models.Notications;
using iParty.Business.Models.Orders;
using iParty.Business.Models.PaymentPlans;
using iParty.Business.Models.People;
using iParty.Business.Models.Review;
using iParty.Business.Models.Users;

namespace iParty.Api.AutoMapper
{
    public class AutoMapperConfiguration : Profile
    {
        public AutoMapperConfiguration()
        {
            CreateMap<Address, AddressDto>().ReverseMap();
            CreateMap<Address, AddressView>().ReverseMap();

            CreateMap<City, CityDto>().ReverseMap();
            CreateMap<City, CityView>().ReverseMap();

            CreateMap<CityForAddress, CityDto>().ReverseMap();
            CreateMap<CityForAddress, CityView>().ReverseMap();

            CreateMap<Person, PersonDto>().ReverseMap();
            CreateMap<Person, PersonView>().ReverseMap();

            CreateMap<Person, CustomerDto>().ReverseMap();
            CreateMap<Person, CustomerView>().ReverseMap();
            
            CreateMap<PersonForOrder, CustomerDto>().ReverseMap();
            CreateMap<PersonForOrder, SupplierDto>().ReverseMap();
            CreateMap<PersonForOrder, Person>().ReverseMap();

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

            CreateMap<Schedule, ScheduleDto>().ReverseMap();
            CreateMap<Schedule, ScheduleView>().ReverseMap();

            CreateMap<ScheduleItem, ScheduleItemDto>().ReverseMap();
            CreateMap<ScheduleItem, ScheduleItemView>().ReverseMap();

            CreateMap<Review, ReviewDto>().ReverseMap();
            CreateMap<Review, ReviewView>().ReverseMap();

            CreateMap<OrderItem, OrderItemView>().ReverseMap();
            
            CreateMap<ItemForOrder, ItemDto>().ReverseMap();
            CreateMap<ItemForOrder, Item>().ReverseMap();

            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<User, UserView>().ReverseMap();

        }
    }
}
