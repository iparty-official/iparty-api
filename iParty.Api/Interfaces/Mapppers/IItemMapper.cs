using iParty.Api.Dtos.Items;
using iParty.Api.Infra;
using iParty.Api.Views.Items;
using iParty.Business.Models.Items;
using System.Collections.Generic;

namespace iParty.Api.Interfaces.Mappers
{
    public interface IItemMapper
    {
        MapperResult<Item> Map(ItemDto dto);

        ItemView Map(Item item);

        List<ItemView> Map(List<Item> items);
    }
}
