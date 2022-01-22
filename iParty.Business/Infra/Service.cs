using iParty.Business.Interfaces;
using iParty.Business.Interfaces.Services;
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
                return ServiceResult<TEntity>.FailureResult("Não foi possível localizar o registro informado.");                

            Rep.Delete(id);

            return ServiceResult<TEntity>.SuccessResult(entity);
        }

        public virtual TEntity Get(Guid id)
        {
            return Rep.RecoverById(id);
        }

        public virtual List<TEntity> Get()
        {
            return Rep.Recover();
        }
    }
}
