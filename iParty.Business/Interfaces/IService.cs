using iParty.Business.Infra;
using iParty.Business.Models;
using System;
using System.Collections.Generic;

namespace iParty.Business.Interfaces
{
    public interface IService<TEntity> where TEntity : Entity
    {
        ServiceResult<TEntity> Delete(Guid id);
        TEntity Get(Guid id);
        List<TEntity> Get();
    }
}
