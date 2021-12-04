using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iParty.Business.Models
{
    public abstract class Entity
    {
        public Guid Id { get; set; }

        public bool Removed { get; set; }

        protected Entity()
        {
            Id = Guid.NewGuid();
            
            Removed = false;
        }
    }
}
