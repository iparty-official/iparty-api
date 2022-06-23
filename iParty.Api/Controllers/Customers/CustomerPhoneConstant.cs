namespace iParty.Api.Controllers.Customers
{
    public class CustomerPhoneConstant
    {
        public const string Tag = "Customer";

        public const string CreateSummary = "It adds a phone to a customer.";
        public const string UpdateSummary = "It updates a customer's phone.";
        public const string DeleteSummary = "It removes a phone from a customer.";

        public const string CreateDescription = "Your app will have a screen where users will inform their own phone numbers. This screen will use this method to persist those numbers.";
        public const string UpdateDescription = "Users can make mistakes when typing phone numbers. Use this method to fix them.";
        public const string DeleteDescription = "Eventually, the user wants to remove a specific phone number and your app will call this method for it.";
    }
}
