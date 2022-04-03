namespace iParty.Api.Controllers.Customers
{
    public class CustomerAddressConstant
    {
        public const string Tag = "Customer";

        public const string CreateSummary = "It adds an address to a customer.";
        public const string UpdateSummary = "It updates a customer's address.";
        public const string DeleteSummary = "It removes an address from a customer.";

        public const string CreateDescription = "Your app will have a screen where users will inform their own shipping addresses. This screen will use this method to persist those addresses.";
        public const string UpdateDescription = "Users can make mistakes when typing addresses. Use this method to fix them.";
        public const string DeleteDescription = "Eventually, the user wants to remove a specific shipping address and your app will call this method for it.";
    }
}
