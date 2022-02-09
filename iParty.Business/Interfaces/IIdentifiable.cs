using System;

namespace iParty.Business.Interfaces
{
    public interface IIdentifiable
    {       
        public Guid Id { get; }

        public Guid Version { get; }

        public bool Removed { get; }

        public void DefineId(Guid id);

        public void DefineIdAndVersion(Guid id, Guid version);

        public void Remove();
    }
}
