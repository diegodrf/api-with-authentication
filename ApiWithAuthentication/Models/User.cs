using System.Text.Json.Serialization;

namespace ApiWithAuthentication.Models
{
    public class User
    {
        public User() { }

        public User(string name, string password, Role role)
        {
            Id = Guid.NewGuid();
            Name = name;
            PasswordHash = CreatePasswordHash(password);
            CreatedAt = DateTime.UtcNow;
            Role = role;
            RoleId = role.Id;
        }

        public Guid Id { get; set; } = default!;
        public string Name { get; set; } = default!;
        public string PasswordHash { get; set; } = default!;
        public DateTime CreatedAt { get; set; } = default!;
        public Role Role { get; set; } = default!;
        [JsonIgnore]
        public int RoleId { get; set; }

        public static string CreatePasswordHash(string password)
        {
            // Fake secutiry
            var security = "###";
            return security + password;
        }

        public static bool IsValidPassword(string PasswordHash, string password)
        {
            return CreatePasswordHash(password) == PasswordHash;
        }
    }
}
