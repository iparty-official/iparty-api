using System;

namespace iParty.Business.Interfaces
{
    public interface IIdentifiable
    {       
        public Guid Id { get; }               
        public void DefineId(Guid id);       
    }
}
