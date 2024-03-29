﻿using iParty.Api.Dtos.Notifications;
using iParty.Api.Infra;
using iParty.Api.Interfaces.Mappers;
using iParty.Business.Infra.Extensions;
using iParty.Business.Models.Notications;
using iParty.Business.Models.People;
using iParty.Business.Interfaces;
using System;
using iParty.Business.Models.Notifications;

namespace iParty.Api.Mappers.Notifications
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

            if (!SuccessResult()) return GetResult();

            SetEntity(new Notification(DateTime.Now, new PersonForNotification(person.Id, person.Name), dto.Text));

            return GetResult();
        }        
    }
}
