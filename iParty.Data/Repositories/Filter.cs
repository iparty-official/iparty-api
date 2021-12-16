using iParty.Business;
using iParty.Business.Interfaces;
using System;
using System.Linq.Expressions;

namespace iParty.Data.Repositories
{
    public class Filter : IFilter
    {
        public static IFilter CreateFilter(object field, FilterOperatorEnum filterOperator, object value) 
        {
            return new Filter() { Field = field, Operator = filterOperator, Value = value };
        }

        public object Field { get; set; }
        public object Value { get; set; }
        public FilterOperatorEnum Operator { get; set; }
    }
}
