using iParty.Business.Infra;
using iParty.Business.Interfaces;
using iParty.Business.Models.Messages;
using iParty.Business.Validations;
using iParty.Data.Repositories;
using System;

namespace iParty.Business.Services.Messages
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

        public ServiceResult<Message> Update(Message message)
        {
            var result = ExecuteValidation(new MessageValidation(), message);

            if (!result.IsValid)
                return GetFailureResult(result);

            Rep.Update(message);

            return GetSuccessResult(message);
        }      
    }
}
