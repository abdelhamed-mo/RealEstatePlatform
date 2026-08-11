using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RealEstateApp.Core.Application.DTOs.Account;
using RealEstateApp.Core.Application.Enums;
using RealEstateApp.Core.Application.Features.Accounts.Commands.RegisterAdminUser;
using RealEstateApp.Core.Application.Features.Accounts.Commands.RegisterDeveloperUser;
using RealEstateApp.Core.Application.Features.Accounts.Queries.Authenticate;
using RealEstateApp.Core.Application.Interfaces.Services;
using Swashbuckle.AspNetCore.Annotations;

namespace RealEstateApp.Presentation.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [SwaggerTag("Account services")]
    public class AccountController : BaseApiController
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost("Authenticate")]
        [SwaggerOperation(
           Summary = "User login",
           Description = "Authenticates a user and returns authentication data."
        )]
        public async Task<IActionResult> AuthenticateAsync([FromBody]AuthenticationRequest request)
        {
            try
            {
                return Ok(await Mediator.Send(new AuthenticateUserQuery { Email = request.Email, Password = request.Password }));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost("RegisterAdminUser")]
        [SwaggerOperation(
           Summary = "Create user with administrator role",
           Description = "Receives the parameters required to create a user with the administrator role."
        )]
        public async Task<IActionResult> RegisterAdminAsync([FromBody]RegisterAdminUserCommand command)
        {
            try
            {
                return Ok(await Mediator.Send(command));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }  
        }

        [HttpPost("RegisterDeveloperUser")]
        [SwaggerOperation(
           Summary = "Create user with developer role",
           Description = "Receives the parameters required to create a user with the developer role."
        )]
        public async Task<IActionResult> RegisterDeveloperAsync([FromBody] RegisterDeveloperUserCommand command)
        {
            try
            {
                return Ok(await Mediator.Send(command));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
