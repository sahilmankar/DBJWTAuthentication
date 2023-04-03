using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using DBJWTAuthenticationCustomMiddleware.Entities;
using DBJWTAuthenticationCustomMiddleware.Helpers;
using DBJWTAuthenticationCustomMiddleware.Models;
using DBJWTAuthenticationCustomMiddleware.Repositories.Interfaces;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MySql.Data.MySqlClient;

namespace DBJWTAuthenticationCustomMiddleware.Repositories
{

    public class UserRepository : IUserRepository
    {
        private readonly AppSettings _appSettings;
        public UserRepository(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;

        }
        private static string connectionString = "server=localhost; database=Authentication; user=root; password=password";
        public AuthenticateResponse Authenticate(AuthenticateRequest request)
        {
            User user = GetUser(request);
            if (user == null)
            {
                return null;
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                // Subject = new ClaimsIdentity(new[] { new Claim("id", user.Id.ToString()) }),
                Subject = new ClaimsIdentity(AllClaims(user)),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var Token = tokenHandler.WriteToken(token);

            AuthenticateResponse response = new AuthenticateResponse(user, Token);
            return response;
        }

        public User GetUser(AuthenticateRequest request)
        {
            User user = null;
            MySqlConnection connection = new MySqlConnection(connectionString);

            try
            {
                string query = "select * from users where userName=@userName and password=@password ";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@userName", request.UserName);
                command.Parameters.AddWithValue("@password", request.Password);
                connection.Open();
                MySqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    int id = int.Parse(reader["userId"].ToString());
                    string firstName = reader["firstName"].ToString();
                    string lastName = reader["lastName"].ToString();
                    string userName = reader["userName"].ToString();

                    user = new User()
                    {
                        Id=id,
                        FirstName = firstName,
                        LastName = lastName,
                        UserName = userName
                    };
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                connection.Close();
            }
            return user;
        }

        private List<Claim>? AllClaims(User user)
        {
            List<Claim> claims = new List<Claim>();
             claims.Add(new Claim("id", user.Id.ToString()));
            List<string> roles = GetRolesOfUser(user);
            foreach (string everyrole in roles)
            {
                claims.Add(new Claim("role", everyrole));
            }
            return claims;
        }


        private List<string> GetRolesOfUser(User user)
        {
            List<string> roles = new List<string>();
            MySqlConnection connection = new MySqlConnection(connectionString);

            try
            {
                string query = " SELECT roles.roleName from roles INNER JOIN userRoles ON roles.roleId=userRoles.roleId " +
                              " INNER JOIN users ON users.userId=userRoles.userId where users.userId=@userId";

                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@userId", user.Id);
                connection.Open();
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    string? roleName = reader["roleName"].ToString();
                    Console.WriteLine(roleName);
                    roles.Add(roleName);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                connection.Close();
            }
            return roles;

        }

        public List<User> GetAllUsers()
        {
            List<User> users = new List<User>();
            MySqlConnection connection = new MySqlConnection(connectionString);

            try
            {
                string query = "select * from users  ";
                MySqlCommand command = new MySqlCommand(query, connection);
                connection.Open();
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    int id = int.Parse(reader["userId"].ToString());
                    string? firstName = reader["firstName"].ToString();
                    string? lastName = reader["lastName"].ToString();
                    string? userName = reader["userName"].ToString();

                    User user = new User()
                    {
                        Id = id,
                        FirstName = firstName,
                        LastName = lastName,
                        UserName = userName
                    };
                    users.Add(user);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                connection.Close();
            }
            return users;
        }

       public User GetById(int userId)
    {
        User user = new User();
        MySqlConnection con = new MySqlConnection();
        con.ConnectionString = connectionString;
        try
        {

            string query = "SELECT * FROM users where userId =@id";
            con.Open();
            MySqlCommand command = new MySqlCommand(query, con);

            command.Parameters.AddWithValue("@id", userId);
           
            MySqlDataReader reader = command.ExecuteReader();

            if (reader.Read())
            {

                    int id = int.Parse(reader["userId"].ToString());
                    string? firstName = reader["firstName"].ToString();
                    string? lastName = reader["lastName"].ToString();
                    string? userName = reader["userName"].ToString();

                     user = new User()
                    {
                        Id = id,
                        FirstName = firstName,
                        LastName = lastName,
                        UserName = userName
                    };
            }

            reader.Close();

        }
        catch (Exception ee)
        {

            throw ee;
        }
        finally
        {
            con.Close();
        }
        Console.WriteLine(user);
        return user;

    }
    }
}