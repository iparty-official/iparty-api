using System.Security.Cryptography;
using System.Text;

namespace iParty.Business.Models.Users
{
    public class User : Entity
    {
        public User(){}
        
        public User(string emailAddress, string password, UserRole role, bool confirmedEmail)
        {
            EmailAddress = emailAddress;
            Password = password;
            Role = role;
            ConfirmedEmail = confirmedEmail;
        }

        public string EmailAddress { get; private set; }
        public string Password { get; private set; }        
        public UserRole Role { get; private set; }
        public bool ConfirmedEmail { get; private set; }

        public void DefineUserRole(UserRole role)
        {
            this.Role = role;
        }

        public void DefineUserPassword(string unhashedPassword)
        {
            this.Password = GeneratePasswordHash(this.EmailAddress, unhashedPassword);
        }

        public static string GeneratePasswordHash(string emailAddress, string password)
        {
            byte[] data = SHA256.Create().ComputeHash(Encoding.UTF8.GetBytes(emailAddress + password));

            var sBuilder = new StringBuilder();

            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            return sBuilder.ToString();
        }
    }
}
