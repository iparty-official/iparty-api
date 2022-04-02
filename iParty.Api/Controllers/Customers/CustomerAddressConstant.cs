namespace iParty.Api.Controllers.Customers
{
    public class CustomerAddressConstant
    {
        public const string Tag = "Customer";

        public const string CreateSummary = "Add an address to a customer.";
        public const string UpdateSummary = "Update a customer's address.";
        public const string DeleteSummary = "Remove an address from a customer.";

        public const string CreateDescription = "Typically you will have a star or heart icon in your app that will should this method to save a bookmark for the user.";
        public const string DeleteDescription = "That same star/heart icon will call this method to remove a user's bookmark.";
        public const string GetByIdDescription = "Honestly, I don't when should you use this method.";
        public const string UpdateDescription = "Please, don't call this method. I'm begging you.";
        public const string GetAllDescription = "Your app must retrieve the bookmark list to show it to the user. Use this method for it.";
    }
}
