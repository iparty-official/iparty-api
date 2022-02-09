using iParty.Business.Infra;
using System;
using System.Collections.Generic;

namespace iParty.Business.Interfaces.Services
{
    public interface IService<TEntity> where TEntity : IIdentifiable
    {
        ServiceResult<TEntity> Delete(Guid id);
        TEntity Get(Guid id);
        List<TEntity> Get();
    }
}
