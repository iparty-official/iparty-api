using iParty.Business.Infra;
using iParty.Business.Models.Users;
using System;

namespace iParty.Business.Interfaces.Services
{
    public interface IUserService : IService<User>
    {
        public ServiceResult<User> Create(User user);

        public ServiceResult<User> Update(Guid id, User user);

        public User Get(string emailAddress, string password);

        public ServiceResult<User> UpgradeToSupplier(Guid id, User user);        
    }
}
