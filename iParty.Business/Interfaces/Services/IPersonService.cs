using iParty.Business.Infra;
using iParty.Business.Models.Addresses;
using iParty.Business.Models.PaymentPlans;
using iParty.Business.Models.People;
using System;

namespace iParty.Business.Interfaces.Services
{
    public interface IPersonService : IService<Person>
    {
        public ServiceResult<Person> Create(Person person);

        public ServiceResult<Person> Update(Guid id, Person person);

        public ServiceResult<Person> AddPhone(Guid personId, Phone phone);

        public ServiceResult<Person> ReplacePhone(Guid personId, Guid phoneId, Phone phone);

        public ServiceResult<Person> RemovePhone(Guid personId, Guid phoneId);

        public ServiceResult<Person> AddAddress(Guid personId, Address address);

        public ServiceResult<Person> ReplaceAddress(Guid personId, Guid addressId, Address address);

        public ServiceResult<Person> RemoveAddress(Guid personId, Guid addressId);

        public ServiceResult<Person> AddPaymentPlan(Guid personId, Guid paymentPlanId);        

        public ServiceResult<Person> RemovePaymentPlan(Guid personId, Guid paymentPlanId);
    }
}
