using iParty.Business.Infra;
using iParty.Business.Interfaces.Notifications;
using iParty.Business.Models.Notications;
using iParty.Business.Validations;
using iParty.Data.Repositories;
using System;
using System.Collections.Generic;

namespace iParty.Business.Services.Notifications
{
    public class NotificationService : Service<Notification, IRepository<Notification>>, INotificationService
    {
        public NotificationService(IRepository<Notification> rep) : base(rep)
        {
        }

        public ServiceResult<Notification> Create(Notification notification)
        {
            var result = ExecuteValidation(new NotificationValidation(), notification);

            if (!result.IsValid)
                return GetFailureResult(result);

            Rep.Create(notification);

            return GetSuccessResult(notification);
        }

        public ServiceResult<Notification> Update(Guid id, Notification notification)
        {
            var currentMessage = Get(id);

            if (currentMessage == null)
                return GetFailureResult("Não foi possível localizar a notificação informada.");

            var result = ExecuteValidation(new NotificationValidation(), notification);

            if (!result.IsValid)
                return GetFailureResult(result);

            Rep.Update(id, notification);

            return GetSuccessResult(notification);
        }
    }
}
