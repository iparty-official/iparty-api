using iParty.Business.Infra;
using iParty.Business.Models;
using iParty.Business.Models.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iParty.Business.Interfaces
{
    public interface IMessageService : IService<Message>
    {
        public ServiceResult<Message> Create(Message message);

        public ServiceResult<Message> Update(Guid id, Message message);
    }
}
