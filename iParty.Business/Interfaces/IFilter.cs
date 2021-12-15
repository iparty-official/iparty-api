using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iParty.Business.Interfaces
{
    public interface IFilter
    {
        public string Field { get; set; }

        public FilterOperatorEnum Operator { get; set; }

        public TPrimitiveType GetValue<TPrimitiveType>();

        public void SetValue<TPrimitiveType>(TPrimitiveType value);
    }
}
