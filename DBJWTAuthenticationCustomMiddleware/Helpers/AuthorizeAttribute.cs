
using DBJWTAuthenticationCustomMiddleware.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace DBJWTAuthenticationCustomMiddleware.Helpers
{

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AuthorizeAttribute : Attribute, IAuthorizationFilter
    {

        public string? Roles { get; set; }
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var user = (User?)context.HttpContext.Items["User"];
            var userRoles = (List<string>?)context.HttpContext.Items["userRoles"];
            bool status = true;

            if (this.Roles != null && userRoles != null)
            {
                List<string> requiredRoles = this.Roles.Split(',').ToList();
                bool result = requiredRoles.Intersect(userRoles).Count() >= 1;
                if (result)
                {
                    status = true;
                }
            }


            if (user == null || status == false)
            {
                context.Result = new JsonResult(new { message = "Unauthorized" })
                {
                    StatusCode = StatusCodes.Status401Unauthorized
                };

            }
        }
    }
}