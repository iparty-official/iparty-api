using iParty.Business.Infra;
using iParty.Business.Interfaces.Services;
using iParty.Business.Interfaces.Validations;
using iParty.Business.Models.Notications;
using iParty.Data.Repositories;

namespace iParty.Business.Services.Notifications
{
    public class NotificationService : Service<Notification, IRepository<Notification>>, INotificationService
    {
        private INotificationValidation _notificationValidation;

        public NotificationService(IRepository<Notification> rep, INotificationValidation notificationValidation) : base(rep)
        {
            _notificationValidation = notificationValidation;
        }

        public ServiceResult<Notification> Create(Notification notification)
        {
            var result = _notificationValidation.Validate(notification);

            if (!result.IsValid)
                return GetFailureResult(result);

            Rep.Create(notification);

            return GetSuccessResult(notification);
        }      
    }
}
