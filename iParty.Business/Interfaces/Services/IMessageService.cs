using iParty.Business.Infra;
using iParty.Business.Models.Messages;
using System;

namespace iParty.Business.Interfaces.Services
{
    public interface IMessageService : IService<Message>
    {
        public ServiceResult<Message> Create(Message message);

        public ServiceResult<Message> Update(Guid id, Message message);
    }
}
