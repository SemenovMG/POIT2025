using Microsoft.AspNetCore.Mvc;
using POIT2025.Dtos.Requests;
using POIT2025.Dtos.Response;
using POIT2025.Entities;
using POIT2025.Services;

namespace POIT2025.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService accountService;
        private readonly ITokenService tokenService;

        public AccountController(IAccountService accountService, ITokenService tokenService)
        {
            this.accountService = accountService;
            this.tokenService = tokenService;
        }


        [HttpPost("register")]
        public async Task<ActionResult<RegistrationResponse>> Register(
            RegisterRequest request)
        {
            try
            {
                UniversityUser? result =
                    await accountService.Register(
                        request.UserName,
                        request.Password);
                return Ok(result);
            }
            catch (ArgumentException ex) 
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("login")]
        public async Task<ActionResult<LoginResponse>> Login(LoginRequest request)
        {
            try
            {
                var user = await accountService.Login(
                    request.UserName,
                    request.Password);

                var response = new LoginResponse
                {
                    Token = tokenService.CreateToken(user!),
                    UserId = user!.Id,
                };

                return Ok(response);
            }
            catch(ArgumentException) 
            {
                return Unauthorized("UserName or Password is not correct");
            }
        }
    }
}
