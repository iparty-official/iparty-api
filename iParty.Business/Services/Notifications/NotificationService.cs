using iParty.Business.Infra;
using iParty.Business.Interfaces.Services;
using iParty.Business.Interfaces.Validations;
using iParty.Business.Models.Notications;
using iParty.Business.Interfaces;
using System.Collections.Generic;
using System;

namespace iParty.Business.Services.Notifications
{
    public class NotificationService : INotificationService
    {
        private BasicService<Notification> _basicService;

        public NotificationService(IRepository<Notification> repository, INotificationValidation notificationValidation)
        {
            _basicService = new BasicService<Notification>(repository, notificationValidation);
        }

        public ServiceResult<Notification> Create(Notification notification)
        {
            return _basicService.Create(notification);
        }

        public ServiceResult<Notification> Update(Guid id, Notification notification)
        {
            return _basicService.Update(id, notification);
        }

        public ServiceResult<Notification> Delete(Guid id)
        {
            return _basicService.Delete(id);
        }

        public Notification Get(Guid id)
        {
            return _basicService.Get(id);
        }

        public List<Notification> Get()
        {
            return _basicService.Get();
        }
    }
}
