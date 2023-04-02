

using DBJWTAuthentication.Entities;
using DBJWTAuthentication.Models;
using DBJWTAuthentication.Repositories.Interfaces;

namespace DBJWTAuthentication.Services
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