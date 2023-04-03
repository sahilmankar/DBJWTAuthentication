
using DBJWTAuthenticationCustomMiddleware.Models;
using DBJWTAuthenticationCustomMiddleware.Helpers;
using DBJWTAuthenticationCustomMiddleware.Services;
using Microsoft.AspNetCore.Mvc;

namespace DBJWTAuthenticationCustomMiddleware.Controller;
[ApiController]
public class DepartmentController : ControllerBase
{

    private readonly IUserService _service;

    public DepartmentController(IUserService service)
    {

        this._service = service;
    }
    
    [HttpPost("department/authenticate")]
    public IActionResult Authenticate([FromBody] AuthenticateRequest request)
    {

        var user = _service.Authenticate(request);

        if (user == null)
            return BadRequest(new { message = "Username or password is incorrect" });

        return Ok(user);
    }

    // [Authorize(Roles=Rolenames.Admin +","+ Rolenames.User)]
    [Authorize(Roles=Rolenames.Distributor)]
    // [Authorize(Roles=Rolenames.Admin)]
    [HttpGet("department/getall")]
    public IActionResult GetAllUsers()
    {
        try
        {
            var message = _service.GetAllUsers();
            if (message == null)
            {
                return BadRequest(new { message = "No Data to Show"});
            }
            else
            {
                return Ok(message);
            }
        }
        catch (Exception e)
        {
            return BadRequest(e);
        }
    }
}
