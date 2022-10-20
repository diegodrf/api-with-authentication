using ApiWithAuthentication.Models;

namespace ApiWithAuthentication.Services.Contracts
{
    public interface ITokenService
    {
        string GenerateToken(User user);
    }
}
