using System;

namespace iParty.Business.Interfaces
{
    public interface IIdentifiable
    {       
        public Guid Id { get; }        

        public bool Removed { get; }

        public void DefineId(Guid id);        

        public void Remove();
    }
}
