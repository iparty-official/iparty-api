using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iParty.Business.Interfaces
{
    public interface IEntity
    {       
        public Guid Id { get; set; }

        public bool Removed { get; set; }        
    }
}
