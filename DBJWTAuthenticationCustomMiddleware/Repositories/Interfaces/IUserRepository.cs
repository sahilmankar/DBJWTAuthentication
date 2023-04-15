
using DBJWTAuthenticationCustomMiddleware.Entities;
using DBJWTAuthenticationCustomMiddleware.Models;

namespace DBJWTAuthenticationCustomMiddleware.Repositories.Interfaces
{
    public interface IUserRepository
    {
        AuthenticateResponse Authenticate(AuthenticateRequest request);
        List<User> GetAllUsers();
        User GetById(int userId);
    }
}