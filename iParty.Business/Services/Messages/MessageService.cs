using iParty.Business.Infra;
using iParty.Business.Interfaces.Services;
using iParty.Business.Interfaces.Validations;
using iParty.Business.Models.Messages;
using iParty.Business.Interfaces;
using System;
using System.Collections.Generic;

namespace iParty.Business.Services.Messages
{
    public class MessageService : IMessageService
    {
        private BasicService<Message> _basicService;

        public MessageService(IRepository<Message> repository, IMessageValidation messageValidation)
        {
            _basicService = new BasicService<Message>(repository, messageValidation);
        }

        public ServiceResult<Message> Create(Message message)
        {
            return _basicService.Create(message);
        }

        public ServiceResult<Message> Update(Guid id, Message message)
        {
            return _basicService.Update(id, message);
        }

        public ServiceResult<Message> Delete(Guid id)
        {
            return _basicService.Delete(id);
        }

        public Message Get(Guid id)
        {
            return _basicService.Get(id);
        }

        public List<Message> Get()
        {
            return _basicService.Get();
        }
    }
}
