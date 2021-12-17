using iParty.Api.Dtos;
using iParty.Api.Infra;
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

        public MapperResult<Notification> Map(NotificationDto dto)
        {
            var result  = new MapperResult<Notification>();
            var person = _personRepository.RecoverById(dto.DestinationId).IfNull(() => result.Errors.Add("O destinatário da notificação não existe."));

            if (!result.Success) 
                return result;

            result.Entity = new Notification
            {
                Date = dto.Date,
                Time = dto.Time,
                Text = dto.Text,
                Destination = person
            };

            return result;
        }        
    }
}
