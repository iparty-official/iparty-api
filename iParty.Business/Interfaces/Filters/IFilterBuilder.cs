using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace iParty.Business.Interfaces.Filters
{
    public interface IFilterBuilder<TEntity> where TEntity : IEntity
    {
        public void Clear();

        public IFilterBuilder<TEntity> Equal(Expression<Func<TEntity, object>> field, object value);

        public IFilterBuilder<TEntity> Unequal(Expression<Func<TEntity, object>> field, object value);

        public IFilterBuilder<TEntity> GreaterThan(Expression<Func<TEntity, object>> field, object value);

        public IFilterBuilder<TEntity> LessThan(Expression<Func<TEntity, object>> field, object value);

        public List<IFilter<TEntity>> Build();        
    }
}
