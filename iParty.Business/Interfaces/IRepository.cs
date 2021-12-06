using iParty.Business.Models;
using System;

namespace iParty.Data.Repositories
{
    public interface IRepository<TEntity> where TEntity : Entity
    {
        public void Create(TEntity entity);

        public void Update(TEntity entity);

        public void Delete(TEntity entity);

        public void Recover(TEntity entity);

        public void RecoverById(TEntity entity);
    }
}
