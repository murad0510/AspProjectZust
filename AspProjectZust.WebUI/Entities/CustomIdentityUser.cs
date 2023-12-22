using Microsoft.AspNetCore.Identity;

namespace AspProjectZust.WebUI.Entities
{
    public class CustomIdentityUser : IdentityUser
    {
        public string? ImageUrl { get; set; }
        public bool IsOnline { get; set; }
        public int Likes { get; set; }
        public int Following { get; set; }
        public int Followers { get; set; }
        public string? City { get; set; }
    }
}
