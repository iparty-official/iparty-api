using AutoMapper;
using iParty.Api.Dtos;
using iParty.Api.Interfaces;
using iParty.Api.Views;
using iParty.Business.Models.Addresses;
using iParty.Business.Models.People;
using iParty.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace iParty.Api.Mappers
{
    public class CustomerMapper : ICustomerMapper
    {
        private IRepository<Person> _personRepository;

        private IMapper _autoMapper;

        private IAddressMapper _addressMapper;

        public CustomerMapper(IRepository<Person> personRepository, IMapper autoMapper, IAddressMapper addressMapper)
        {
            _personRepository = personRepository;
            _autoMapper = autoMapper;
            _addressMapper = addressMapper;
        }

        private Person getPerson(Guid id, string notFoundMessage)
        {
            var person = _personRepository.RecoverById(id);

            if (person == null)
            {
                throw new Exception(notFoundMessage);
            }

            return person;
        }

        public Person Map(CustomerDto dto)
        {
            var person = new Person()
            {                
                User = dto.User,
                Name = dto.Name,
                Document = dto.Document,
                Photo = dto.Photo,
                SupplierOrCustomer = SupplierOrCustomer.Customer,
                CustomerInfo = new Customer() { BirthDate = dto.BirthDate },
                Addresses = new List<Address>(),
                Phones = new List<Phone>()
            };

            person.Addresses.AddRange(dto.Addresses.Select(x => _addressMapper.Map(x)));

            person.Phones.AddRange(dto.Phones.Select(x => _autoMapper.Map<Phone>(x)));

            return person;
        }

        public CustomerView Map(Person person)
        {
            if (person == null) return null;
            
            var customerView = new CustomerView()
            {
                Id = person.Id,
                User = person.User,
                Name = person.Name,
                Document = person.Document,
                Photo = person.Photo,                
                BirthDate = person.CustomerInfo.BirthDate,
                Addresses = new List<AddressView>(),
                Phones = new List<PhoneView>()
            };

            customerView.Addresses.AddRange(person.Addresses.Select(x => _autoMapper.Map<AddressView>(x)));

            customerView.Phones.AddRange(person.Phones.Select(x => _autoMapper.Map<PhoneView>(x)));

            return customerView;
        }

        public List<CustomerView> Map(List<Person> people)
        {
            var customers = new List<CustomerView>();

            foreach (var person in people)
            {               
                var customerView = new CustomerView()
                {
                    Id = person.Id,
                    User = person.User,
                    Name = person.Name,
                    Document = person.Document,
                    Photo = person.Photo,                    
                    BirthDate = person.CustomerInfo.BirthDate,
                    Addresses = new List<AddressView>(),
                    Phones = new List<PhoneView>()
                };

                customerView.Addresses.AddRange(person.Addresses.Select(x => _autoMapper.Map<AddressView>(x)));

                customerView.Phones.AddRange(person.Phones.Select(x => _autoMapper.Map<PhoneView>(x)));

                customers.Add(customerView);
            }


            return customers;
        }
    }
}
