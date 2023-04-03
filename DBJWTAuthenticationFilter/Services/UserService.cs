

using DBJWTAuthenticationFilter.Entities;
using DBJWTAuthenticationFilter.Models;
using DBJWTAuthenticationFilter.Repositories.Interfaces;

namespace DBJWTAuthenticationFilter.Services
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
    }
}