﻿using iParty.Business.Infra;
using iParty.Business.Models.Notications;

namespace iParty.Business.Interfaces.Services
{
    public interface INotificationService : IService<Notification>
    {
        ServiceResult<Notification> Create(Notification notification);       
    }
}
