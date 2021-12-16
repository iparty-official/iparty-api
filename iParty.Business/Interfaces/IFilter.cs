using System;
using System.Linq.Expressions;

namespace iParty.Business.Interfaces
{
    public interface IFilter<TEntity> where TEntity : IEntity
    {        
        public Expression<Func<TEntity, object>> Field { get; set; }
        public object Value { get; set; }
        public FilterOperatorEnum Operator { get; set; }

        public string GetFieldName(Expression<Func<TEntity, object>> field);
    }
}
