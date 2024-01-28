using Microsoft.AspNetCore.Identity;

namespace NetBanking.Infrastructure.Identity.Entities
{
    public class AppUser : IdentityUser
    {
        public string Name { get; set; } 
        public string LastName { get; set; } 
        public string Cedula { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
