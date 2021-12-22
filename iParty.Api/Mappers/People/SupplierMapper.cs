using AutoMapper;
using iParty.Api.Dtos.People;
using iParty.Api.Infra;
using iParty.Api.Interfaces.Mappers;
using iParty.Api.Views.Addresses;
using iParty.Api.Views.PaymentPlans;
using iParty.Api.Views.People;
using iParty.Business.Infra.Extensions;
using iParty.Business.Models.Addresses;
using iParty.Business.Models.PaymentPlans;
using iParty.Business.Models.People;
using iParty.Data.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace iParty.Api.Mappers.People
{
    public class SupplierMapper : BaseMapper<Person>, ISupplierMapper
    {       
        private IMapper _autoMapper;

        private IAddressMapper _addressMapper;

        private IRepository<PaymentPlan> _paymentPlanRepository;

        public SupplierMapper(IMapper autoMapper, IAddressMapper addressMapper, IRepository<PaymentPlan> paymentPlanRepository)
        {            
            _autoMapper = autoMapper;
            _addressMapper = addressMapper;
            _paymentPlanRepository = paymentPlanRepository;
        }

        public MapperResult<Person> Map(SupplierDto dto)
        {
            var person = new Person()
            {
                User = dto.User,
                Name = dto.Name,
                Document = dto.Document,
                Photo = dto.Photo,
                SupplierOrCustomer = SupplierOrCustomer.Supplier,
                Phones = new List<Phone>(),
                Addresses = new List<Address>(),
                CustomerInfo = new Customer(),
                SupplierInfo = new Supplier() { BusinessDescription = dto.BusinessDescription, PaymentPlans = new List<PaymentPlan>() }
            };            

            person.Phones.AddRange(dto.Phones.Select(x => _autoMapper.Map<Phone>(x)));
           
            person.Addresses.AddRange(dto.Addresses.Select(x =>
            {
                var mapperResult = _addressMapper.Map(x);

                if (!mapperResult.Success)
                    foreach (var erro in mapperResult.Errors) AddError(erro);

                return mapperResult.Entity;
            }));

            person.SupplierInfo.PaymentPlans.AddRange(dto.PaymentPlans.Select(x =>
            {
                var paymentPlan = _paymentPlanRepository.RecoverById(x).IfNull(() => { AddError("Plano de pagamento informado não foi encontrado."); });                

                return paymentPlan;
            }));

            SetEntity(person);

            return GetResult();
        }

        public SupplierView Map(Person person)
        {            
            return mapPersonToSupplierView(person);
        }

        public List<SupplierView> Map(List<Person> people)
        {
            var suplliers = new List<SupplierView>();

            foreach (var person in people)
            {                               
                suplliers.Add(mapPersonToSupplierView(person));
            }

            return suplliers;
        }

        private SupplierView mapPersonToSupplierView(Person person)
        {
            if (person == null) return null;

            var supplierView = new SupplierView()
            {
                Id = person.Id,
                User = person.User,
                Name = person.Name,
                Document = person.Document,
                Photo = person.Photo,
                BusinessDescription = person.SupplierInfo.BusinessDescription,
                Addresses = new List<AddressView>(),
                Phones = new List<PhoneView>(),
                PaymentPlans = new List<PaymentPlanView>()
            };

            supplierView.Addresses.AddRange(person.Addresses.Select(x => _autoMapper.Map<AddressView>(x)));

            supplierView.Phones.AddRange(person.Phones.Select(x => _autoMapper.Map<PhoneView>(x)));

            supplierView.PaymentPlans.AddRange(person.SupplierInfo.PaymentPlans.Select(x => _autoMapper.Map<PaymentPlanView>(x)));

            return supplierView;
        }
    }
}
