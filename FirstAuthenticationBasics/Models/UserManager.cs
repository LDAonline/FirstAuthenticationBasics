using System.Security.Claims;

namespace FirstAuthenticationBasics.Models
{
    //Only static because demo app 
    //In real implelmentation -users would obviously becoming from db
    public static class UserManager
    {
        private static  List<UserAccount> _accounts;

        static UserManager()
        {
            _accounts = [
                new UserAccount
                {
                    Username = "admin",
                    Password = "testing123",
                    Claims = new List<Claim> 
                    {
                        new Claim(ClaimTypes.Name, "admin"),
                        new Claim("admin", "true")
                    }
                },
                new UserAccount
                {
                    Username = "JohnDoe",
                    Password = "testing123",
                    Claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, "JohnDoe"),
                    }
                }
                ];
        }

        public static UserAccount Login(string username, string password)
        {
            return _accounts.FirstOrDefault(x => x.Username == username && x.Password == password);
        }
    }

    public class UserAccount
    { 
        public string Username { get; set; }    

        public string Password { get; set; }    

        //A claim is a key-value pair that contains data about a specific user
        //Can contain info like if user is an admin or any other relevant information for authorisation
        public List<Claim> Claims { get; set; }

    }


}
