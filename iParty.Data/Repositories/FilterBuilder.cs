using iParty.Business.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iParty.Data.Repositories
{
    public class FilterBuilder : IFilterBuilder
    {
        private List<IFilter> _filters;

        public FilterBuilder()
        {
            _filters = new List<IFilter>();
        }

        public IFilterBuilder Equal(object field, object value)
        {
            _filters.Add(new Filter() { Field = field, Operator = FilterOperatorEnum.Equals, Value = value });

            return this;
        }

        public IFilterBuilder GreaterThan(object field, object value)
        {
            _filters.Add(new Filter() { Field = field, Operator = FilterOperatorEnum.GreaterThan, Value = value });

            return this;
        }

        public IFilterBuilder LessThan(object field, object value)
        {
            _filters.Add(new Filter() { Field = field, Operator = FilterOperatorEnum.LessThan, Value = value });

            return this;
        }

        public List<IFilter> Build()
        {
            return _filters;
        }
    }
}
