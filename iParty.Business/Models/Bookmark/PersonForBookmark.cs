using System;

namespace iParty.Business.Models.Bookmark
{
    public class PersonForBookmark
    {
        public PersonForBookmark()
        {
        }
        public PersonForBookmark(Guid id, string name)
        {
            Id = id;
            Name = name;
        }
        public Guid Id { get; private set; }
        public string Name { get; private set; }
    }
}