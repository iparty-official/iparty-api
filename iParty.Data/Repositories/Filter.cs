using iParty.Business;
using iParty.Business.Interfaces;
using System;
using System.Linq.Expressions;

namespace iParty.Data.Repositories
{
    public class Filter<TEntity> : IFilter<TEntity> where TEntity : IEntity
    {
        public Expression<Func<TEntity, object>> Field { get; set; }
        public object Value { get; set; }
        public FilterOperatorEnum Operator { get; set; }
    }
}
