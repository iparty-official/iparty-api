using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iParty.Business.Models
{
    public abstract class Entity
    {
        protected Entity()
        {
            Id = Guid.NewGuid();

            Removed = false;
        }

        public Guid Id { get; set; }

        public bool Removed { get; set; }
       
        public void Remove()
        {
            Removed = true;
        }
    }
}
