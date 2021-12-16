using iParty.Api.Dtos;
using iParty.Api.Interfaces;
using iParty.Business.Infra.Extensions;
using iParty.Business.Models.Notications;
using iParty.Business.Models.People;
using iParty.Data.Repositories;
using System;

namespace iParty.Api.Mappers
{
    public class NotificationMapper : INotificationMapper
    {
        private IRepository<Person> _personRepository;
        public NotificationMapper(IRepository<Person> personRepository)
        {
            _personRepository = personRepository;
        }
        public Notification Map(NotificationDto dto)
        {
            return new Notification
            {
                Date = dto.Date,
                Time = dto.Time,
                Text = dto.Text,
                Destination = _personRepository.RecoverById(dto.DestinationId).ExceptionIfNull("O destinatário da notificação não existe.")
            };
        }        
    }
}
