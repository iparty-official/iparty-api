using System;

namespace iParty.Business.Interfaces
{
    public interface IEntity
    {       
        public Guid Id { get; set; }

        public Guid Version { get; set; }

        public bool Removed { get; set; }        
    }
}
