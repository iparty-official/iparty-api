using System;

namespace iParty.Business.Models.People
{
    public class Phone
    {
        public Phone() 
        {
            Id = Guid.NewGuid();
        }
        public Phone(Guid id, string prefix, string number)
        {
            Id = id;
            Prefix = prefix;
            Number = number;
        }
        public Guid Id { get; private set; }
        public string Prefix { get; private set; }
        public string Number { get; private set; }

    }
}
