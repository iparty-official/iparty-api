using iParty.Business.Models;
using System;
using System.Collections.Generic;

namespace iParty.Business.Infra
{
    public interface IService<TEntity> where TEntity : Entity
    {
        void Delete(Guid id);
        TEntity Get(Guid id);
        List<TEntity> Get();
    }
}
