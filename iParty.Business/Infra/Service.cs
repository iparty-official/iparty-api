using iParty.Business.Interfaces;
using iParty.Business.Interfaces.Services;
using iParty.Data.Repositories;
using System;
using System.Collections.Generic;

namespace iParty.Business.Infra
{
    //TODO: Abstrair serviços. Criar branch separada.
    public class Service<TEntity, TRepository> : BaseService<TEntity, TRepository>, IService<TEntity>
        where TEntity : IEntity, new()
        where TRepository : IRepository<TEntity>        
    {
        public Service(TRepository rep) : base(rep)
        {
        }
        public virtual ServiceResult<TEntity> Delete(Guid id)
        {
            var entity = Get(id);

            if (entity == null)
                return GetFailureResult("Não foi possível localizar o registro informado.");                

            Rep.Delete(id);

            return GetSuccessResult(entity);
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
