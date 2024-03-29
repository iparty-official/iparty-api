﻿using FluentValidation;
using iParty.Business.Interfaces;
using iParty.Business.Interfaces.Filters;
using iParty.Business.Interfaces.Validations;
using iParty.Business.Models.Users;
using System.Text.RegularExpressions;

namespace iParty.Business.Validations
{
    public class UserValidation : AbstractValidator<User>, IUserValidation
    {
        public UserValidation(IRepository<User> userRepository, IFilterBuilder<User> filterBuilder)
        {
            RuleFor(p => p.EmailAddress).NotEmpty().WithMessage("Endereço de e-mail não foi informado.");

            RuleFor(p => isEmailValid(p.EmailAddress)).Equal(true).WithMessage("Endereço de e-mail informado é inválido.");

            RuleFor(p => p.Password).NotEmpty().WithMessage("Senha não foi informada.");

            RuleFor(p => doesPasswordRespectPolicy(p.Password)).Equal(true).WithMessage("Senha informada não respeita a política de senhas.");

            RuleFor(p => p.Role).IsInEnum().WithMessage("Papel do usuário é inválido.");

            RuleFor(p => userAlreadyExists(userRepository, filterBuilder, p)).Equal(false).WithMessage("Já existe um usuário cadastrado com o endereço de e-mail informado.");
        }

        private bool userAlreadyExists(IRepository<User> userRepository, IFilterBuilder<User> filterBuilder, User user)
        {            
            filterBuilder
                .Equal(x => x.EmailAddress, user.EmailAddress)
                .Unequal(x => x.Id, user.Id);

            return userRepository.Recover(filterBuilder).Count > 0;
        }

        private bool doesPasswordRespectPolicy(string password)
        {            
            return true;
        }

        private bool isEmailValid(string emailAddress)
        {
            string pattern = @"^(?!\.)(""([^""\r\\]|\\[""\r\\])*""|" + @"([-a-z0-9!#$%&'*+/=?^_`{|}~]|(?<!\.)\.)*)(?<!\.)" + @"@[a-z0-9][\w\.-]*[a-z0-9]\.[a-z][a-z\.]*[a-z]$";
            var regex = new Regex(pattern, RegexOptions.IgnoreCase);
            return regex.IsMatch(emailAddress);
        }       
    }
}
