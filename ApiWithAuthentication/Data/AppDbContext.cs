using ApiWithAuthentication.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiWithAuthentication.Data
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            
        }

        public DbSet<User> Users => Set<User>();
        public DbSet<Role> Roles => Set<Role>();
    }
}
