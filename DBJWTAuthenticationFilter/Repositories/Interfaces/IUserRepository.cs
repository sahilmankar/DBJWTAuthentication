
using DBJWTAuthenticationFilter.Entities;
using DBJWTAuthenticationFilter.Models;

namespace DBJWTAuthenticationFilter.Repositories.Interfaces
{
    public interface IUserRepository
    {

        AuthenticateResponse Authenticate(AuthenticateRequest request);
        List<User> GetAllUsers();

    }
}