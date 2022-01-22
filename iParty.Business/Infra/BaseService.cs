using FluentValidation.Results;
using iParty.Business.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace iParty.Business.Infra
{
    public abstract class BaseService<TEntity, TRepository> where TEntity : IEntity, new() where TRepository : IRepository<TEntity>
    {
        protected BaseService(TRepository rep)
        {
            Rep = rep;
        }

        protected TRepository Rep { get; private set; }
    }
}
