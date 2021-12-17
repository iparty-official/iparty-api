using iParty.Api.Dtos;
using iParty.Api.Interfaces;
using iParty.Business.Models.People;
using iParty.Data.Repositories;
using System;

namespace iParty.Api.Mappers
{
    public class CustomerMapper : ICustomerMapper
    {
        private IRepository<Person> _personRepository;

        public CustomerMapper(IRepository<Person> personRepository)
        {
            _personRepository = personRepository;
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
            return new Person()
            {
                User = null,
                Name = null,
                Document = null,
                Photo = null,
                SupplierOrCustomer = 0,
                CustomerInfo = null,
                SupplierInfo = null,
                Addresses = null,
                Phones = null
            };
        }
    }
}
