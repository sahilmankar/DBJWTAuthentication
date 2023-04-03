using System.Text.Json.Serialization;
using DBJWTAuthenticationCustomMiddleware.Entities;

namespace DBJWTAuthenticationCustomMiddleware.Models
{
    public class AuthenticateResponse
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        [JsonIgnore]
        public string? UserName { get; set; }

        [JsonIgnore]
        public string? Password { get; set; }
        public string? Token { get; set; }


        public AuthenticateResponse(User user, string token)
        {
            Id = user.Id;
            FirstName = user.FirstName;
            LastName = user.LastName;
            this.Token = token;
        }
    }
}