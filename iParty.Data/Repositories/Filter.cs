using iParty.Business.Interfaces;
using iParty.Business.Interfaces.Filters;
using System;
using System.Linq.Expressions;

namespace iParty.Data.Repositories
{
    public class Filter<TEntity> : IFilter<TEntity> where TEntity : IIdentifiable
    {
        public Filter(Expression<Func<TEntity, object>> field, FilterOperatorEnum @operator, object value)
        {
            Field = field;
            Operator = @operator;
            Value = value;
        }

        public Expression<Func<TEntity, object>> Field { get; private set; }
        public FilterOperatorEnum Operator { get; private set; }

        public object Value { get; private set; }
        
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
