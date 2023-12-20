using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AspProjectZust.WebUI.Entities
{
    public class CustomIdenityDbContext : IdentityDbContext
    {
        public CustomIdenityDbContext(DbContextOptions<CustomIdenityDbContext> options)
            :base(options)
        {
                                                                           
        }                                                                  
    }                                                                      
}                                                                          
                                                                           