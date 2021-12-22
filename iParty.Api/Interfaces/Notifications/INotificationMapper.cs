using iParty.Api.Dtos.Notifications;
using iParty.Api.Infra;
using iParty.Business.Models.Notications;

namespace iParty.Api.Interfaces.Notifications
{
    public interface INotificationMapper
    {
        MapperResult<Notification> Map(NotificationDto dto);
    }
}
