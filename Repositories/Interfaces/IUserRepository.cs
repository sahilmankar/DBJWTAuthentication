
using DBJWTAuthentication.Entities;
using DBJWTAuthentication.Models;

namespace DBJWTAuthentication.Repositories.Interfaces
{
    public interface IUserRepository
    {

        AuthenticateResponse Authenticate(AuthenticateRequest request);
        List<User> GetAllUsers();

    }
}