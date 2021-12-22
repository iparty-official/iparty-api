using iParty.Api.Dtos.Messages;
using iParty.Api.Interfaces.Messages;
using iParty.Business.Models.Messages;
using iParty.Business.Models.People;
using iParty.Data.Repositories;
using System;

namespace iParty.Api.Infra.Messages
{
    public class MessageMapper : IMessageMapper
    {
        private IRepository<Person> _personRepository;

        public MessageMapper(IRepository<Person> personRepository)
        {
            _personRepository = personRepository;
        }

        private Person getPerson(Guid id, string notFoundMessage)
        {
            var person = _personRepository.RecoverById(id);

            if (person == null)
            {
                throw new Exception(notFoundMessage);
            }

            return person;
        }

        public Message Map(MessageDto dto)
        {           
            return new Message()
            {
                DateTime = dto.DateTime,
                From = getPerson(dto.FromId, "O remetente da mensagem não existe."),
                To = getPerson(dto.ToId, "O destinatário da mensagem não existe."),
                Text = dto.Text,
                Order = null
            };
        }        
    }
}
