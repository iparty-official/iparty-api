using iParty.Business.Interfaces;
using System;

namespace iParty.Business.Models
{
    public abstract class Entity: IIdentifiable, IVersionable, IRemoveble
    {
        protected Entity()
        {
            Id = Guid.NewGuid();

            Version = Guid.NewGuid();

            Removed = false;
        }

        protected Entity(Guid id, Guid version, bool removed)
        {
            Id = id;
            Version = version;
            Removed = removed;
        }

        public Guid Id { get; private set; }

        public Guid Version { get; private set; }

        public bool Removed { get; private set; }

        public void DefineId(Guid id)
        {
            this.Id = id;            
        }

        public void DefineIdAndVersion(Guid id, Guid version) 
        {
            this.Id = id;
            this.Version = version;
        }

        public void Remove()
        {
            this.Removed = false;
        }
    }
}
