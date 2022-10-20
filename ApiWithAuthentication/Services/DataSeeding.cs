using ApiWithAuthentication.Data;
using ApiWithAuthentication.Models;
using System.Runtime.CompilerServices;

namespace ApiWithAuthentication.Services
{
    public class DataSeeding
    {
        private readonly AppDbContext _context;

        public DataSeeding(AppDbContext context)
        {
            _context = context;
        }

        public async Task RunAsync()
        {
            var roles = new List<Role>
            {
                new Role { Id = 1, Name = "Admin" },
                new Role { Id = 2, Name = "User" },
                new Role { Id = 3, Name = "SuperAdmin" }
            };

            await _context.Roles.AddRangeAsync(roles);

            var adminUser = new User("Diego", "1234", roles.First(_ => _.Id == 1))
            {
                Id = new Guid("749E2202-ABA1-4DB2-9298-96A8267D42C8")
            };

            var userUser = new User("Camila", "1234", roles.First(_ => _.Id == 2))
            {
                Id = new Guid("157AC07E-DCD6-4B60-BC66-691B05BDF356")
            };


            await _context.Users.AddAsync(adminUser);
            await _context.Users.AddAsync(userUser);

            await _context.SaveChangesAsync();

        }
    }
}
