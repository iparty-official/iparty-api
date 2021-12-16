using iParty.Api.Dtos;
using iParty.Business.Models.Notications;

namespace iParty.Api.Interfaces
{
    public interface INotificationMapper
    {
        Notification Map(NotificationDto dto);
    }
}
