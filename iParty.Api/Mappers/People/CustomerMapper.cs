using AutoMapper;
using iParty.Api.Dtos.People;
using iParty.Api.Infra;
using iParty.Api.Interfaces.Mappers;
using iParty.Api.Views.Addresses;
using iParty.Api.Views.People;
using iParty.Business.Models.Addresses;
using iParty.Business.Models.PaymentPlans;
using iParty.Business.Models.People;
using System;
using System.Collections.Generic;
using System.Linq;

namespace iParty.Api.Mappers.People
{
    public class CustomerMapper : BaseMapper<Person>, ICustomerMapper
    {
        private readonly IMapper _autoMapper;

        private readonly IAddressMapper _addressMapper;

        public CustomerMapper(IMapper autoMapper, IAddressMapper addressMapper)
        {
            _autoMapper = autoMapper;
            _addressMapper = addressMapper;
        }

        public MapperResult<Person> Map(CustomerDto dto)
        {            
            var person = new Person
            (                
                dto.Name,
                dto.Document,                
                SupplierOrCustomer.Customer,
                new Customer(dto.BirthDate == DateTime.MinValue ? null : dto.BirthDate),
                new Supplier(String.Empty, new List<PaymentPlan>())                
            );

            if (!SuccessResult())
            {
                return GetResult();
            }

            SetEntity(person);

            return GetResult();
        }

        public CustomerView Map(Person person)
        {
            return mapToView(person);
        }

        public List<CustomerView> Map(List<Person> people)
        {
            var customers = new List<CustomerView>();

            foreach (var person in people)
            {
                customers.Add(mapToView(person));
            }

            return customers;
        }

        private CustomerView mapToView(Person person)
        {
            if (person == null)
            {
                return null;
            }

            var customerView = new CustomerView()
            {
                Id = person.Id,
                Version = person.Version,                
                Name = person.Name,
                Document = person.Document,                
                BirthDate = person.CustomerInfo.BirthDate
            };

            return customerView;
        }        
    }
}
