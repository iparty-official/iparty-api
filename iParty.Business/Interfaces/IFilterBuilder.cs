using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iParty.Business.Interfaces
{
    public interface IFilterBuilder
    {
        public IFilterBuilder Equal(object field, object value);

        public IFilterBuilder GreaterThan(object field, object value);

        public IFilterBuilder LessThan(object field, object value);

        public List<IFilter> Build();        
    }
}
