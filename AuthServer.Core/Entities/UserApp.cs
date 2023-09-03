using Microsoft.AspNetCore.Identity;

namespace AuthServer.Core.Entities
{
    public class UserApp : IdentityUser
    {
        public string City { get; set; }
        public DateTime BirthDate { get; set; }
    }
}
