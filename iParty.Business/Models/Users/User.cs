namespace iParty.Business.Models.Users
{
    public class User : Entity
    {
        public string EmailAddress { get; set; }
        public string Password { get; set; }        
        public UserRole Role { get; set; }
        public bool ConfirmedEmail { get; set; }        
    }
}
