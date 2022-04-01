namespace iParty.Api.Controllers.Bookmarks
{
    public static class BookmarkConstant
    {
        public const string Tag = "Bookmark";

        public const string CreateSummary = "Create a bookmark for the user.";        
        public const string DeleteSummary = "Remove a user's bookmark.";
        public const string GetByIdSummary = "Get a specific bookmark of an user.";
        public const string GetAllSummary = "Get all the user's bookmarks.";
               
        public const string CreateDescription = "Typically you will have a star or heart icon in your app that will should this method to save a bookmark for the user.";       
        public const string DeleteDescription = "That same star/heart icon will call this method to remove a user's bookmark.";       
        public const string GetByIdDescription = "Honestly, I don't when should you use this method.";
        public const string GetAllDescription = "Your app must retrieve the bookmark list to show it to the user. Use this method for it.";
    }
}
