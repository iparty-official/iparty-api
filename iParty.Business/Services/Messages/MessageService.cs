using iParty.Business.Infra;
using iParty.Business.Interfaces.Messages;
using iParty.Business.Models.Messages;
using iParty.Business.Validations;
using iParty.Data.Repositories;
using System;

namespace iParty.Business.Services.Cities
{
    public class MessageService : Service<Message, IRepository<Message>>, IMessageService
    {
        public MessageService(IRepository<Message> rep) : base(rep)
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
                return GetFailureResult("Não foi possível localizar a mensagem informada.");

            var result = ExecuteValidation(new MessageValidation(), message);

            if (!result.IsValid)
                return GetFailureResult(result);

            Rep.Update(id, message);

            return GetSuccessResult(message);
        }
    }
}
