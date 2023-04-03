

using DBJWTAuthenticationCustomMiddleware.Entities;
using DBJWTAuthenticationCustomMiddleware.Models;
using DBJWTAuthenticationCustomMiddleware.Repositories.Interfaces;

namespace DBJWTAuthenticationCustomMiddleware.Services
{

    public class UserService : IUserService
    {
        private readonly IUserRepository _repo;

        public UserService(IUserRepository repo)
        {
            _repo = repo;
        }

        public AuthenticateResponse Authenticate(AuthenticateRequest request)
        {
            return _repo.Authenticate(request);
        }



        public List<User> GetAllUsers()
        {
            return _repo.GetAllUsers();
        }

        public User GetById(int userId)
        {
            return _repo.GetById(userId);
        }
    }
}