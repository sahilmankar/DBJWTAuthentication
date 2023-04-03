
using System.Security.Cryptography;
using DBJWTAuthenticationCustomMiddleware.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace DBJWTAuthenticationCustomMiddleware.Helpers
{

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        public AuthorizeAttribute()
        {
        }
        public   string? Roles {get;set;}
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var user = (User)context.HttpContext.Items["User"];
            var userRoles = (List<string>)context.HttpContext.Items["userRoles"];

          
                Console.WriteLine(this.Roles);
            
            if (user == null )
            {
                context.Result = new JsonResult(new { message = "Unauthorized" })
                {
                    StatusCode = StatusCodes.Status401Unauthorized
                };

            }

            bool IsRoleAdmin()
            {
                foreach (var role in userRoles)
                {

                }
                return false;
            }
        }
    }


}