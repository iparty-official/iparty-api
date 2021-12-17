using iParty.Business.Infra;
using iParty.Business.Models.People;
using System;

namespace iParty.Business.Interfaces
{
    public interface IPersonService : IService<Person>
    {
        public ServiceResult<Person> Create(Person person);

        public ServiceResult<Person> Update(Guid id, Person person);
    }
}
