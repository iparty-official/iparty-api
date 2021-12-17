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
    public class NotificationMapper : BaseMapper<Notification>, INotificationMapper
    {
        private IRepository<Person> _personRepository;

        public NotificationMapper(IRepository<Person> personRepository)
        {
            _personRepository = personRepository;
        }

        public MapperResult<Notification> Map(NotificationDto dto)
        {            
            var person = _personRepository.RecoverById(dto.DestinationId).IfNull(() => AddError("O destinatário da notificação não existe."));

            if (!ResultIsValid()) 
                return GetResult();

            SetEntity(new Notification
            {
                Date = dto.Date,
                Time = dto.Time,
                Text = dto.Text,
                Destination = person
            });

            return GetResult();
        }        
    }
}
