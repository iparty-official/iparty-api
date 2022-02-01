namespace iParty.Business.Models.People
{
    public class Phone: Entity
    {
        public Phone() { }
        public Phone(string prefix, string number)
        {
            Prefix = prefix;
            Number = number;
        }
        public string Prefix { get; private set; }
        public string Number { get; private set; }

    }
}
