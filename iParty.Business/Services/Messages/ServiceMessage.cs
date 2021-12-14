using iParty.Business.Infra;
using iParty.Business.Interfaces;
using iParty.Business.Models.Messages;
using iParty.Business.Validations;
using iParty.Data.Repositories;
using System;
using System.Collections.Generic;

namespace iParty.Business.Services.Cities
{
    public class ServiceMessage : Service<Message, IRepository<Message>>, IServiceMessage
    {
        public ServiceMessage(IRepository<Message> rep) : base(rep)
        {
        }

        public ServiceResult<Message> Create(Message message)
        {
            var result = ExecuteValidation(new MessageValidation(), message);

            if (!result.IsValid)
                return GetFailureResult(result);

            Rep.Create(message);

            return GetSuccessResult(message);
        }

        public ServiceResult<Message> Update(Guid id, Message message)
        {
            var currentMessage = Get(id);

            if (currentMessage == null)
                return new ServiceResult<Message>
                {
                    Success = false,
                    Entity = null,
                    Errors = new List<string> { "Não foi possível localizar a cidade informada." }
                };

            var result = ExecuteValidation(new MessageValidation(), message);

            if (!result.IsValid)
                return GetFailureResult(result);

            Rep.Update(id, message);

            return GetSuccessResult(message);
        }
    }
}
