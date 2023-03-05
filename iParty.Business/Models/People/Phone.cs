using System;

namespace iParty.Business.Models.People
{
    public class Phone : Entity
    {
        public Phone() : base()
        {
        }

        public Phone(Guid id, string prefix, string number) : base(id, Guid.NewGuid(), false)
        {                     
            Prefix = prefix;
            Number = number;
        }      
        
        public string Prefix { get; private set; }
        public string Number { get; private set; }

    }
}
