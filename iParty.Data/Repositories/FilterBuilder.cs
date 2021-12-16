using iParty.Business.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace iParty.Data.Repositories
{
    public class FilterBuilder<TEntity> : IFilterBuilder<TEntity> where TEntity : IEntity
    {
        private List<IFilter<TEntity>> _filters;

        public FilterBuilder()
        {
            _filters = new List<IFilter<TEntity>>();
        }

        public IFilterBuilder<TEntity> Equal(Expression<Func<TEntity, object>> field, object value)
        {
            _filters.Add(new Filter<TEntity>() { Field = field, Operator = FilterOperatorEnum.Equal, Value = value });

            return this;
        }

        public IFilterBuilder<TEntity> Unequal(Expression<Func<TEntity, object>> field, object value)
        {
            _filters.Add(new Filter<TEntity>() { Field = field, Operator = FilterOperatorEnum.Unequal, Value = value });

            return this;
        }

        public IFilterBuilder<TEntity> GreaterThan(Expression<Func<TEntity, object>> field, object value)
        {
            _filters.Add(new Filter<TEntity>() { Field = field, Operator = FilterOperatorEnum.GreaterThan, Value = value });

            return this;
        }

        public IFilterBuilder<TEntity> LessThan(Expression<Func<TEntity, object>> field, object value)
        {
            _filters.Add(new Filter<TEntity>() { Field = field, Operator = FilterOperatorEnum.LessThan, Value = value });

            return this;
        }

        public List<IFilter<TEntity>> Build()
        {
            return _filters;
        }
    }
}
