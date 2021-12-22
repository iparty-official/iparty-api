using iParty.Business.Infra;
using iParty.Business.Models.Notications;
using System;

namespace iParty.Business.Interfaces.Notifications
{
    public interface INotificationService : IService<Notification>
    {
        ServiceResult<Notification> Create(Notification notification);
        ServiceResult<Notification> Update(Guid id, Notification notification);
    }
}
