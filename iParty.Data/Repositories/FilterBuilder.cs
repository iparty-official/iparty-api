using iParty.Business.Interfaces;
using iParty.Business.Interfaces.Filters;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace iParty.Data.Repositories
{
    public class FilterBuilder<TEntity> : IFilterBuilder<TEntity> where TEntity : IEntity
    {
        private List<IFilter<TEntity>> _filters;

        public FilterBuilder()
        {
            _filters = new List<IFilter<TEntity>>();
        }

        public void Clear()
        {
            _filters.Clear();            
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
