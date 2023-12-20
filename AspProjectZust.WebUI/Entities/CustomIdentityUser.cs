using Microsoft.AspNetCore.Identity;

namespace AspProjectZust.WebUI.Entities
{
    public class CustomIdentityUser : IdentityUser
    {
        public string? ImageUrl { get; set; }
        public bool IsOnline { get; set; }
    }
}
