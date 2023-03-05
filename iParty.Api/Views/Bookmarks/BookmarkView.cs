using iParty.Api.Views.Items;
using iParty.Api.Views.People;
using System;

namespace iParty.Api.Views.Bookmarks
{
    public class BookmarkView : View
    {
        public Guid CustomerId { get; set; }
        public Guid ItemId { get; set; }
        public DateTime DateTime { get; set; }
    }
}
