using iParty.Api.Views.Items;
using iParty.Api.Views.Orders;
using iParty.Api.Views.People;
using System;

namespace iParty.Api.Views.Bookmarks
{
    public class BookmarkView : View
    {
        public PersonSummarizedView Customer { get; set; }
        public ItemSummarizedView Item { get; set; }        
        public DateTime DateTime { get; set; }        
    }
}
