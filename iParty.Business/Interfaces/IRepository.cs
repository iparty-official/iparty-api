using iParty.Business.Models;
using System;
using System.Collections.Generic;

namespace iParty.Data.Repositories
{
    public interface IRepository<TEntity> where TEntity : Entity
    {
        public void Create(TEntity entity);

        public void Update(TEntity entity);

        public void Delete(Guid id);

        public ICollection<TEntity> Recover();

        public TEntity RecoverById(Guid id);
    }
}
