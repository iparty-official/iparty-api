using FluentValidation;
using iParty.Business.Interfaces;
using iParty.Business.Models;
using iParty.Data.Repositories;
using System;
using System.Collections.Generic;

namespace iParty.Business.Infra
{
    public class Service<TEntity, TRepository> : BaseService<TEntity, TRepository>, IService<TEntity>
        where TEntity : Entity, new()
        where TRepository : IRepository<TEntity>        
    {
        public Service(TRepository rep) : base(rep)
        {
        }
        public virtual ServiceResult<TEntity> Delete(Guid id)
        {
            var entity = Get(id);
            if (entity == null)
                return new ServiceResult<TEntity>
                {
                    Success = false,
                    Entity = null,
                    Errors = new List<string> { "Não foi possível localizar o registro informado." }
                };

            Rep.Delete(id);

            return GetSuccessResult(null);
        }

        public TEntity Get(Guid id)
        {
            return Rep.RecoverById(id);
        }

        public List<TEntity> Get()
        {
            return Rep.Recover();
        }
    }
}
