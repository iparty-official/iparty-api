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
            var addresses = mapAddresses(dto);

            var paymentPlans = mapnPaymentPlans(dto);

            var person = new Person()
            {
                User = dto.User,
                Name = dto.Name,
                Document = dto.Document,
                Photo = dto.Photo,
                SupplierOrCustomer = SupplierOrCustomer.Supplier,
                Phones = dto.Phones.Select(x => _autoMapper.Map<Phone>(x)).ToList(),
                Addresses = addresses,
                CustomerInfo = new Customer(),
                SupplierInfo = new Supplier() { BusinessDescription = dto.BusinessDescription, PaymentPlans = paymentPlans }
            };

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
                User = person.User,
                Name = person.Name,
                Document = person.Document,
                Photo = person.Photo,
                BusinessDescription = person.SupplierInfo.BusinessDescription,
                Addresses = person.Addresses.Select(x => _autoMapper.Map<AddressView>(x)).ToList(),
                Phones = person.Phones.Select(x => _autoMapper.Map<PhoneView>(x)).ToList(),
                PaymentPlans = person.SupplierInfo.PaymentPlans.Select(x => _autoMapper.Map<PaymentPlanView>(x)).ToList()
            };

            return supplierView;
        }

        private List<Address> mapAddresses(SupplierDto dto)
        {
            var mapperResultList = _addressMapper.Map(dto.Addresses);

            if (mapperResultList.Exists(x => !x.Success))
            {
                foreach (var mapperResult in mapperResultList)
                {
                    foreach (var erro in mapperResult.Errors)
                    {
                        AddError(erro);
                    }
                }

                return mapperResultList.Select(x => x.Entity).ToList();
            }
            else
            {
                return new List<Address>();
            }
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
