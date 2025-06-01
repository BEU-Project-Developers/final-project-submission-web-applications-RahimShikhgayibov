using Microsoft.AspNetCore.Identity;

namespace Restaurant.WebUi.Models
{
    public class AppUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
