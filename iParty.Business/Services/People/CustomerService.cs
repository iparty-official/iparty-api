using iParty.Business.Interfaces.Filters;
using iParty.Business.Interfaces.Services;
using iParty.Business.Interfaces.Validations;
using iParty.Business.Models.PaymentPlans;
using iParty.Business.Models.People;
using iParty.Business.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace iParty.Business.Services.People
{
    public class CustomerService : PersonService, ICustomerService
    {
        public CustomerService(IRepository<Person> rep, IFilterBuilder<Person> personFilterBuilder, IPersonValidation personValidation, IPhoneValidation phoneValidation, IPersonPhoneValidation personPhoneValidation, IAddressValidation addressValidation, IPersonAddressValidation personAddressValidation, IRepository<PaymentPlan> paymentPlanRepository) : base(rep, personFilterBuilder, personValidation, phoneValidation, personPhoneValidation, addressValidation, personAddressValidation, paymentPlanRepository)
        {
        }

        public override Person Get(Guid id)
        {
            PersonFilterBuilder.Clear();

            PersonFilterBuilder
                .Equal(x => x.Id, id)
                .Equal(x => x.SupplierOrCustomer, SupplierOrCustomer.Customer);

            return Rep.Recover(PersonFilterBuilder).FirstOrDefault();
        }

        public override List<Person> Get()
        {
            PersonFilterBuilder.Clear();

            PersonFilterBuilder.Equal(x => x.SupplierOrCustomer, SupplierOrCustomer.Customer);

            return Rep.Recover(PersonFilterBuilder);
        }
    }
}
