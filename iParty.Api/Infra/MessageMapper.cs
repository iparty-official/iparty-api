using iParty.Api.Dtos;
using iParty.Api.Interfaces;
using iParty.Business.Models.People;
using iParty.Business.Models.Messages;
using iParty.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace iParty.Api.Infra
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
                Date = dto.Date,
                From = getPerson(dto.FromId, "O remetente da mensagem não existe."),
                Order = null,
                Text = dto.Text,
                Time = dto.Time,
                To = getPerson(dto.FromId, "O destinatário da mensagem não existe.")
            };
        }
    }
}
