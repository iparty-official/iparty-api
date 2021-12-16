using System;
using System.Linq.Expressions;

namespace iParty.Business.Interfaces
{
    public interface IFilter
    {
        public static IFilter CreateFilter(object field, FilterOperatorEnum filterOperator, object value) => null;
        public object Field { get; set; }
        public object Value { get; set; }
        public FilterOperatorEnum Operator { get; set; }
    }
}
