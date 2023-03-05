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

        public ServiceResult<Phone> AddPhone(Guid personId, Phone phone);

        public ServiceResult<Phone> ReplacePhone(Guid personId, Guid phoneId, Phone phone);

        public ServiceResult<Phone> RemovePhone(Guid personId, Guid phoneId);

        public ServiceResult<Address> AddAddress(Guid personId, Address address);

        public ServiceResult<Address> ReplaceAddress(Guid personId, Guid addressId, Address address);

        public ServiceResult<Address> RemoveAddress(Guid personId, Guid addressId);

        public ServiceResult<PaymentPlan> AddPaymentPlan(Guid personId, Guid paymentPlanId);        

        public ServiceResult<PaymentPlan> RemovePaymentPlan(Guid personId, Guid paymentPlanId);
    }
}
