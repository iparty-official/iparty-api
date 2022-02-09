using System;

namespace iParty.Business.Interfaces
{
    public interface IVersionable : IIdentifiable
    {
        public Guid Version { get; }

        public void DefineIdAndVersion(Guid id, Guid version);
    }
}
