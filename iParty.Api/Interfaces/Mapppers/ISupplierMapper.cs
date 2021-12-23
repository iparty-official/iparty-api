using iParty.Api.Dtos.People;
using iParty.Api.Infra;
using iParty.Api.Views.People;
using iParty.Business.Models.People;
using System.Collections.Generic;

namespace iParty.Api.Interfaces.Mappers
{
    public interface ISupplierMapper
    {
        MapperResult<Person> Map(SupplierDto dto);

        SupplierView Map(Person person);

        List<SupplierView> Map(List<Person> people);
    }
}
