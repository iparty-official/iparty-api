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
        private IMapper _autoMapper;

        private IAddressMapper _addressMapper;        

        public CustomerMapper(IMapper autoMapper, IAddressMapper addressMapper)
        {            
            _autoMapper = autoMapper;
            _addressMapper = addressMapper;
        }        

        public MapperResult<Person> Map(CustomerDto dto)
        {
            var addresses = mapAddresses(dto);

            var person = new Person()
            {                
                User = dto.User,
                Name = dto.Name,
                Document = dto.Document,
                Photo = dto.Photo,
                SupplierOrCustomer = SupplierOrCustomer.Customer,
                CustomerInfo = new Customer() { BirthDate = dto.BirthDate == DateTime.MinValue ? null : dto.BirthDate },
                SupplierInfo = new Supplier() { PaymentPlans = new List<PaymentPlan>() },
                Phones = dto.Phones.Select(x => _autoMapper.Map<Phone>(x)).ToList(),
                Addresses = addresses
            };                        

            if (!SuccessResult()) return GetResult();           

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
            if (person == null) return null;

            var customerView = new CustomerView()
            {
                Id = person.Id,
                User = person.User,
                Name = person.Name,
                Document = person.Document,
                Photo = person.Photo,
                BirthDate = person.CustomerInfo.BirthDate,
                Addresses = person.Addresses.Select(x => _autoMapper.Map<AddressView>(x)).ToList(),
                Phones = person.Phones.Select(x => _autoMapper.Map<PhoneView>(x)).ToList()
            };            

            return customerView;
        }

        private List<Address> mapAddresses(CustomerDto dto)
        {
            var mapperResultList = _addressMapper.Map(dto.Addresses);

            if (mapperResultList.Exists(x => !x.Success))
            {
                foreach (var mapperResult in mapperResultList)
                    foreach (var erro in mapperResult.Errors) AddError(erro);

                return mapperResultList.Select(x => x.Entity).ToList();
            }
            else
                return new List<Address>();
        }
    }
}
