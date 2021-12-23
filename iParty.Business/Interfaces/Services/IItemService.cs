using iParty.Business.Infra;
using iParty.Business.Models.Items;
using System;

namespace iParty.Business.Interfaces.Services
{
    public interface IItemService : IService<Item>
    {
        public ServiceResult<Item> Create(Item item);

        public ServiceResult<Item> Update(Guid id, Item item);

        public ServiceResult<Item> AddSchedule(Guid itemId, Schedule schedule);

        public ServiceResult<Item> ReplaceSchedule(Guid itemId, Guid scheduleId, Schedule schedule);

        public ServiceResult<Item> RemoveSchedule(Guid itemId, Guid scheduleId);        
    }
}
