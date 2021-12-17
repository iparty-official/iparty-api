using iParty.Api.Dtos;
using iParty.Api.Infra;
using iParty.Business.Models.Notications;

namespace iParty.Api.Interfaces
{
    public interface INotificationMapper
    {
        MapperResult<Notification> Map(NotificationDto dto);
    }
}
