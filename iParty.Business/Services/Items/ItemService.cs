using iParty.Business.Infra;
using iParty.Business.Interfaces.Services;
using iParty.Business.Interfaces.Validations;
using iParty.Business.Models.Items;
using iParty.Business.Interfaces;
using System;

namespace iParty.Business.Services.Items
{  
    public class ItemService : Service<Item, IRepository<Item>>, IItemService
    {                
        private IItemValidation _itemValidation;

        private IScheduleValidation _scheduleValidation;

        private IItemScheduleValidation _itemScheduleValidation;

        public ItemService(IRepository<Item> rep,                           
                           IItemValidation itemValidation,
                           IScheduleValidation scheduleValidation,
                           IItemScheduleValidation itemScheduleValidation) : base(rep)
        {            
            _itemValidation = itemValidation;
            _scheduleValidation = scheduleValidation;
            _itemScheduleValidation = itemScheduleValidation;
        }

        public ServiceResult<Item> Create(Item item)
        {
            var result = _itemValidation.CustomValidate(item);

            if (!result.IsValid)
                return GetFailureResult(result);            
                            
            Rep.Create(item);

            return GetSuccessResult(item);
        }

        public ServiceResult<Item> Update(Guid id, Item item)
        {
            var currentItem = Get(id);

            if (currentItem == null)
                return GetFailureResult("Não foi possível localizar o item informado.");

            var result = _itemValidation.Validate(item);

            if (!result.IsValid)
                return GetFailureResult(result);

            Rep.Update(id, item);

            return GetSuccessResult(item);
        }

        public ServiceResult<Item> AddSchedule(Guid itemId, Schedule schedule)
        {
            var item = Get(itemId);

            if (item == null)
                return GetFailureResult("Não foi possível localizar o item informado.");

            var result = _scheduleValidation.Validate(schedule);

            if (!result.IsValid)
                return GetFailureResult(result);

            item.Schedules.Add(schedule);

            result = _itemScheduleValidation.Validate(item);

            if (!result.IsValid)
                return GetFailureResult(result);

            Rep.Update(itemId, item);

            return GetSuccessResult(item);
        }

        public ServiceResult<Item> ReplaceSchedule(Guid itemId, Guid scheduleId, Schedule schedule)
        {
            var item = Get(itemId);

            if (item == null)
                return GetFailureResult("Não foi possível localizar o item informado.");

            var result = _scheduleValidation.Validate(schedule);

            if (!result.IsValid)
                return GetFailureResult(result);

            item.ReplaceSchedule(scheduleId, schedule);

            result = _itemScheduleValidation.Validate(item);

            if (!result.IsValid)
                return GetFailureResult(result);

            Rep.Update(itemId, item);

            return GetSuccessResult(item);
        }

        public ServiceResult<Item> RemoveSchedule(Guid itemId, Guid scheduleId)
        {
            var item = Get(itemId);

            if (item == null)
                return GetFailureResult("Não foi possível localizar o item informado.");

            item.RemoveSchedule(scheduleId);

            var result = _itemScheduleValidation.Validate(item);

            if (!result.IsValid)
                return GetFailureResult(result);

            Rep.Update(itemId, item);

            return GetSuccessResult(item);
        }

        public ServiceResult<Item> IncreaseAvailableQuantity(Guid itemId, decimal quantity)
        {
            var item = Get(itemId);

            if (item == null)
                return GetFailureResult("Não foi possível localizar o item informado.");

            item.ProductInfo.AvailableQuantity += quantity;

            Rep.Update(itemId, item);

            return GetSuccessResult(item);
        }

        public ServiceResult<Item> DecreaseAvailableQuantity(Guid itemId, decimal quantity)
        {
            var item = Get(itemId);

            if (item == null)
                return GetFailureResult("Não foi possível localizar o item informado.");

            if ((item.ProductInfo.AvailableQuantity - quantity) < 0)
                return GetFailureResult("Não foi possível diminuir o estoque do item, pois sua quantidade ficaria negativa.");

            item.ProductInfo.AvailableQuantity -= quantity;

            Rep.Update(itemId, item);

            return GetSuccessResult(item);
        }        
    }
}
