using AutoMapper;
using iParty.Api.Dtos.Items;
using iParty.Api.Infra;
using iParty.Api.Interfaces.Mappers;
using iParty.Api.Views.Items;
using iParty.Api.Views.People;
using iParty.Business.Infra.Extensions;
using iParty.Business.Models.Items;
using iParty.Business.Models.People;
using iParty.Data.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace iParty.Api.Mappers.Items
{
    public class ItemMapper : BaseMapper<Item>, IItemMapper
    {
        private IMapper _autoMapper;        

        private IRepository<Person> _personRepository;

        public ItemMapper(IMapper autoMapper, IRepository<Person> personRepository)
        {
            _autoMapper = autoMapper;            
            _personRepository = personRepository;
        }

        public MapperResult<Item> Map(ItemDto dto)
        {
            var supplier = _personRepository.RecoverById(dto.SupplierId).IfNull(() => { AddError("O fornecedor informado não existe."); });

            if (!SuccessResult()) return GetResult();

            var item = new Item()
            {
                Supplier = supplier,
                Name = dto.Name,
                Details = dto.Details,
                Photo = dto.Photo,
                Price = dto.Price,
                Unit = dto.Unit,
                ProductOrService = dto.ProductOrService,
                ProductInfo = new Product() { AvailableQuantity = dto.AvailableQuantity, ForRentOrSale = dto.ForRentOrSale },
                Schedules = new List<Schedule>()
            };

            item.Schedules.AddRange(dto.Schedules.Select(x => _autoMapper.Map<Schedule>(x)));

            SetEntity(item);

            return GetResult();
        }

        public ItemView Map(Item item)
        {
            return mapItemToItemView(item);
        }

        public List<ItemView> Map(List<Item> items)
        {
            var result = new List<ItemView>();

            foreach (var item in items)
            {
                result.Add(mapItemToItemView(item));
            }

            return result;
        }

        private ItemView mapItemToItemView(Item item)
        {
            if (item == null) return null;

            var supplier = new PersonSummarizedView() { Id = item.Supplier.Id, Name = item.Supplier.Name };

            var itemView = new ItemView()
            {
                Id = item.Id,
                Supplier = supplier,
                Name = item.Name,
                Details = item.Details,
                Photo = item.Photo,
                Price = item.Price,
                Unit = item.Unit,
                ProductOrService = item.ProductOrService,
                AvailableQuantity = item.ProductInfo.AvailableQuantity,
                ForRentOrSale = item.ProductInfo.ForRentOrSale,
                Schedules = new List<ScheduleView>()
            };            

            itemView.Schedules.AddRange(item.Schedules.Select(x => _autoMapper.Map<ScheduleView>(x)));

            return itemView;            
        }

    }
}
