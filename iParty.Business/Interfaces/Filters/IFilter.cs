﻿using System;
using System.Linq.Expressions;

namespace iParty.Business.Interfaces.Filters
{
    public interface IFilter<TEntity> where TEntity : IEntity
    {        
        public Expression<Func<TEntity, object>> Field { get; }
        public object Value { get; }
        public FilterOperatorEnum Operator { get; }

        public string GetFieldName(Expression<Func<TEntity, object>> field);
    }
}
