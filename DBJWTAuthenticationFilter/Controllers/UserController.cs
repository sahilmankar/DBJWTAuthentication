
using DBJWTAuthenticationFilter.Helpers;
using DBJWTAuthenticationFilter.Models;
using DBJWTAuthenticationFilter.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace DBJWTAuthenticationFilter.Controller;
[Authorize]
[ApiController]
public class DepartmentController : ControllerBase
{

    private readonly IUserService _service;

    public DepartmentController(IUserService service)
    {

        this._service = service;
    }

    [AllowAnonymous]
    [HttpPost("department/authenticate")]
    public IActionResult Authenticate([FromBody] AuthenticateRequest request)
    {

        var user = _service.Authenticate(request);

        if (user == null)
            return BadRequest(new { message = "Username or password is incorrect" });

        return Ok(user);
    }


    [Authorize(Roles = Rolenames.Admin)]
    [HttpGet("department/getall")]

    public IActionResult GetAllUsers()
    {

        try
        {
            var message = _service.GetAllUsers();
            if (message == null)
            {
                return BadRequest(new { message = "No Data to Show" });
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
