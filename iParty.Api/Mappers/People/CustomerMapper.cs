using AutoMapper;
using iParty.Api.Dtos.People;
using iParty.Api.Infra;
using iParty.Api.Interfaces.Mappers;
using iParty.Api.Views.Addresses;
using iParty.Api.Views.People;
using iParty.Business.Models.Addresses;
using iParty.Business.Models.PaymentPlans;
using iParty.Business.Models.People;
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
            var person = new Person()
            {                
                User = dto.User,
                Name = dto.Name,
                Document = dto.Document,
                Photo = dto.Photo,
                SupplierOrCustomer = SupplierOrCustomer.Customer,
                CustomerInfo = new Customer() { BirthDate = dto.BirthDate },
                SupplierInfo = new Supplier() { PaymentPlans = new List<PaymentPlan>() },
                Phones = new List<Phone>(),
                Addresses = new List<Address>()                
            };

            person.Phones.AddRange(dto.Phones.Select(x => _autoMapper.Map<Phone>(x)));

            person.Addresses.AddRange(dto.Addresses.Select(x => 
            { 
                var mapperResult = _addressMapper.Map(x);

                if (!mapperResult.Success)                 
                    foreach (var erro in mapperResult.Errors) AddError(erro);
               
                return mapperResult.Entity;
            }));

            if (!SuccessResult()) return GetResult();           

            SetEntity(person);

            return GetResult();
        }        

        public CustomerView Map(Person person)
        {            
            return mapPersonToCustomerView(person);
        }

        public List<CustomerView> Map(List<Person> people)
        {
            var customers = new List<CustomerView>();

            foreach (var person in people)
            {                                               
                customers.Add(mapPersonToCustomerView(person));
            }


            return customers;
        }

        private CustomerView mapPersonToCustomerView(Person person)
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

    }
}
