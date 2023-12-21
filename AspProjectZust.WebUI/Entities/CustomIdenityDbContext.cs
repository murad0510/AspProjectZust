using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AspProjectZust.WebUI.Entities
{
    public class CustomIdenityDbContext : IdentityDbContext<CustomIdentityUser, CustomIdentityRole, string>
    {
        public CustomIdenityDbContext(DbContextOptions<CustomIdenityDbContext> options)
            : base(options)
        {

        }
    }
}
