using iParty.Business.Infra;
using iParty.Business.Interfaces;
using iParty.Business.Interfaces.Filters;
using iParty.Business.Interfaces.Services;
using iParty.Business.Interfaces.Validations;
using iParty.Business.Models.Users;
using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace iParty.Business.Services.Users
{
    public class UserService : Service<User, IRepository<User>>, IUserService
    {        
        private IUserValidation _userValidation;

        private IFilterBuilder<User> _filterBuilder;

        public UserService(IRepository<User> rep, IUserValidation userValidation, IFilterBuilder<User> filterBuilder) : base(rep)
        {
            _userValidation = userValidation;
            _filterBuilder = filterBuilder;
        }

        public ServiceResult<User> Create(User user)
        {
            user.Role = UserRole.Customer;
            user.Password = GeneratePasswordHash(user.EmailAddress, user.Password);

            var result = _userValidation.Validate(user);

            if (!result.IsValid)
                return GetFailureResult(result);

            Rep.Create(user);

            return GetSuccessResult(user);
        }

        public ServiceResult<User> Update(Guid id, User user)
        {
            var currentUser = Get(user.Id);

            if (currentUser == null)
                return GetFailureResult("Não foi possível localizar o usuário informado.");

            user.Password = GeneratePasswordHash(user.EmailAddress, user.Password);

            var result = _userValidation.Validate(user);

            if (!result.IsValid)
                return GetFailureResult(result);

            Rep.Update(id, user);

            return GetSuccessResult(user);
        }

        public User Get(string emailAddress, string password)
        {
            var hash = GeneratePasswordHash(emailAddress, password);

            _filterBuilder
                .Equal(x => x.EmailAddress, emailAddress)
                .Equal(x => x.Password, hash);

            return Rep.Recover(_filterBuilder).FirstOrDefault();
        }

        public ServiceResult<User> UpgradeToSupplier(Guid id, User user)
        {
            var currentUser = Get(user.Id);

            if (currentUser == null)
                return GetFailureResult("Não foi possível localizar o usuário informado.");

            currentUser.Role = UserRole.Supplier;

            var result = _userValidation.Validate(user);

            if (!result.IsValid)
                return GetFailureResult(result);

            Rep.Update(id, user);

            return GetSuccessResult(user);
        }

        public string GeneratePasswordHash(string emailAddress, string password)
        {
            byte[] data = SHA256.Create().ComputeHash(Encoding.UTF8.GetBytes(emailAddress + password));

            var sBuilder = new StringBuilder();

            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            return sBuilder.ToString();
        }
    }
}
