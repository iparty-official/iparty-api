﻿using iParty.Api.Dtos.Messages;
using iParty.Api.Interfaces.Mappers;
using iParty.Business.Infra.Extensions;
using iParty.Business.Models.Messages;
using iParty.Business.Models.People;
using iParty.Business.Interfaces;
using System;

namespace iParty.Api.Infra.Messages
{
    public class MessageMapper : BaseMapper<Message>, IMessageMapper
    {
        private IRepository<Person> _personRepository;

        public MessageMapper(IRepository<Person> personRepository)
        {
            _personRepository = personRepository;
        }

        public MapperResult<Message> Map(MessageDto dto)
        {
            var from = _personRepository.RecoverById(dto.FromId).IfNull(() => { AddError("O remetente da mensagem não existe."); });
            
            var to = _personRepository.RecoverById(dto.ToId).IfNull(() => { AddError("O destinatário da mensagem não existe."); });

            if (!SuccessResult()) return GetResult();

            SetEntity (new Message(
                new PersonForMessage(from.Id, from.Name),
                new PersonForMessage(to.Id, to.Name),
                dto.Text,
                DateTime.Now,
                dto.OrderId
                ));

            return GetResult();
        }        
    }
}
