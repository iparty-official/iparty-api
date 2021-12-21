using iParty.Business.Infra;
using iParty.Business.Models.People;
using System;

namespace iParty.Business.Interfaces
{
    public interface IPersonService : IService<Person>
    {
        public ServiceResult<Person> Create(Person person);

        public ServiceResult<Person> Update(Guid id, Person person);

        public ServiceResult<Person> AddPhone(Guid personId, Phone phone);

        public ServiceResult<Person> ReplacePhone(Guid personId, Guid phoneId, Phone phone);

        public ServiceResult<Person> RemovePhone(Guid personId, Guid phoneId);
    }
}
