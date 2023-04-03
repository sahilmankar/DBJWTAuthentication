
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
            var user = (User?)context.HttpContext.Items["User"];
            var userRoles = (List<string>?)context.HttpContext.Items["userRoles"];

            var requiredRoles= this.Roles?.Split(',').ToList();

           bool status=false;
           foreach(var role in requiredRoles){
                if(userRoles.Contains(role)){
                    status=true;
                }else{
                    status=false;
                }
           }

            if (user == null || status==false )
            {
                context.Result = new JsonResult(new { message = "Unauthorized" })
                {
                    StatusCode = StatusCodes.Status401Unauthorized
                };

            }
        }
    }
}