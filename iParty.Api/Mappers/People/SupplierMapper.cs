using AutoMapper;
using iParty.Api.Dtos.People;
using iParty.Api.Infra;
using iParty.Api.Interfaces.Mappers;
using iParty.Api.Views.Addresses;
using iParty.Api.Views.PaymentPlans;
using iParty.Api.Views.People;
using iParty.Business.Infra.Extensions;
using iParty.Business.Interfaces;
using iParty.Business.Models.Addresses;
using iParty.Business.Models.PaymentPlans;
using iParty.Business.Models.People;
using System.Collections.Generic;
using System.Linq;

namespace iParty.Api.Mappers.People
{
    public class SupplierMapper : BaseMapper<Person>, ISupplierMapper
    {
        private readonly IMapper _autoMapper;

        private readonly IAddressMapper _addressMapper;

        private readonly IRepository<PaymentPlan> _paymentPlanRepository;

        public SupplierMapper(IMapper autoMapper, IAddressMapper addressMapper, IRepository<PaymentPlan> paymentPlanRepository)
        {
            _autoMapper = autoMapper;
            _addressMapper = addressMapper;
            _paymentPlanRepository = paymentPlanRepository;
        }

        public MapperResult<Person> Map(SupplierDto dto)
        {            
            var paymentPlans = mapnPaymentPlans(dto);

            var person = new Person(               
                dto.Name,
                dto.Document,                
                SupplierOrCustomer.Supplier,
                new Customer(null),
                new Supplier(dto.BusinessDescription, paymentPlans)
            );

            if (!SuccessResult())
            {
                return GetResult();
            }

            SetEntity(person);

            return GetResult();
        }

        public SupplierView Map(Person person)
        {
            return mapToView(person);
        }

        public List<SupplierView> Map(List<Person> people)
        {
            var suplliers = new List<SupplierView>();

            foreach (var person in people)
            {
                suplliers.Add(mapToView(person));
            }

            return suplliers;
        }

        private SupplierView mapToView(Person person)
        {
            if (person == null)
            {
                return null;
            }

            var supplierView = new SupplierView()
            {
                Id = person.Id,
                Version = person.Version,                
                Name = person.Name,
                Document = person.Document,                
                BusinessDescription = person.SupplierInfo.BusinessDescription,                
                PaymentPlanIds = person.SupplierInfo.PaymentPlans.Select(x => x.Id).ToList()
            };

            return supplierView;
        }        

        private List<PaymentPlan> mapnPaymentPlans(SupplierDto dto)
        {
            return dto.PaymentPlans.Select(x =>
            {
                var paymentPlan = _paymentPlanRepository.RecoverById(x).IfNull(() => { AddError("O plano de pagamento informado não foi encontrado."); });

                return paymentPlan;
            }).ToList();
        }
    }
}
