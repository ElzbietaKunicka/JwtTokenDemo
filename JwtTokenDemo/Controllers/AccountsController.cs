using JwtTokenDemo.BL;
using JwtTokenDemo.DTOS;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JwtTokenDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountService _accountService;
        public AccountsController(IAccountService accountService) 
        { 
            _accountService = accountService;
        }

        [HttpPost("SignUp")]
        public ActionResult Signup(AuthRequestDto request)
        {
            _accountService.SignupNewAccount(request.UserName, request.Password);
            return Ok();
        }

        [HttpPost("Login")]
        public ActionResult Login(AuthRequestDto request)
        {
            var loginSuccess = _accountService.Login(request.UserName, request.Password);   

            if(!loginSuccess) 
            {
                return BadRequest("Invalid username or password");
            }
            else
            {
                // todo generate jwt
                return Ok();
            }
            
        }
    }
}
