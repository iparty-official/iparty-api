using iParty.Business.Interfaces;
using System;

namespace iParty.Business.Models
{
    public abstract class Entity: IEntity
    {
        protected Entity()
        {
            Id = Guid.NewGuid();

            Removed = false;
        }

        public Guid Id { get; set; }

        public bool Removed { get; set; }              
    }
}
