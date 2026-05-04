using Microsoft.AspNetCore.Identity;

namespace UniGuide.Data
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; } = string.Empty;

        public string Role { get; set; } = string.Empty;

        public string Major { get; set; } = string.Empty;
    }
}