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

        public string GetFieldName(Expression<Func<TEntity, object>> field)
        {
            MemberExpression body = field.Body as MemberExpression;

            if (body == null)
            {
                UnaryExpression ubody = (UnaryExpression)field.Body;

                body = ubody.Operand as MemberExpression;
            }

            return body.Member.Name;
        }
    }
}
