using System;

namespace iParty.Business.Models.Bookmark
{
    public class ItemForBookmark
    {
        public ItemForBookmark()
        {
        }
        public ItemForBookmark(Guid id, string sku, string name, object photo)
        {
            Id = id;
            SKU = sku;
            Name = name;
            Photo = photo;
        }
        public Guid Id { get; private set; }
        public string SKU { get; private set; }
        public string Name { get; private set; }        
        public object Photo { get; private set; }        
    }
}