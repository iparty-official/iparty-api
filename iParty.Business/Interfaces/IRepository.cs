using iParty.Business.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace iParty.Data.Repositories
{
    public interface IRepository<TEntity> where TEntity : IEntity
    {
        public void Create(TEntity entity);

        public void Update(Guid id, TEntity entity);

        void Delete(Guid id);

        public List<TEntity> Recover();

        public List<TEntity> Recover(IFilterBuilder filterBuilder);

        public List<TEntity> Recover(Expression<Func<TEntity, object>> field, object value);

        public TEntity RecoverById(Guid id);
    }
}
