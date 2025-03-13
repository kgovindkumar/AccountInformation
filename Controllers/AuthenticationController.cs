using AccountService.Interface;
using AccountService.Model;
using Microsoft.AspNetCore.Mvc;

namespace AccountService.Controllers
{
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        public readonly IAuthenticationManager _authenticationManager;
        public AuthenticationController(IAuthenticationManager authenticationManager)
        {
            _authenticationManager = authenticationManager;
        }

        [HttpPost("authenticateuser")]
        public IActionResult AuthenticateUser(UserDetail userDetail)
        {
            var token = _authenticationManager.Authenticate(userDetail);
            return Ok(token);
        }
    }
}
