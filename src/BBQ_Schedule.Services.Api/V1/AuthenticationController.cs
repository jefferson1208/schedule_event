using BBQ_Schedule.Domain.Interfaces;
using BBQ_Schedule.Infra.Identity;
using BBQ_Schedule.Infra.Identity.Authentication;
using BBQ_Schedule.Services.Api.Controllers;
using BBQ_Schedule.Services.Api.ViewModels.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BBQ_Schedule.Services.Api.V1
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}")]
    public class AuthenticationController : MainController
    {
        private readonly IAuthenticationService _authenticationService;
        public AuthenticationController(INotifier notifier, IUser appUser,
            IAuthenticationService authenticationService) : base(notifier, appUser)
        {
           _authenticationService = authenticationService;
        }

        [HttpPost("register")]
        public async Task<ActionResult<LoginResponseViewModel>> Register(RegisterUserViewModel userRegister)
        {
            if (!ModelState.IsValid) return CustomizeResponse(ModelState);

            var register = await _authenticationService.Register(userRegister.Email, userRegister.Password);

            if(register)
            {
                var response = await _authenticationService.GenerateJwt(userRegister.Email);

                return new LoginResponseViewModel
                {
                    AccessToken = response.AccessToken,
                    ExpiresIn = response.ExpiresIn
                };

			}

            return CustomizeResponse();
        }

        [HttpPost("login")]
        public async Task<ActionResult<LoginResponseViewModel>> Login(LoginUserViewModel user)
        {
            if (!ModelState.IsValid) return CustomizeResponse(ModelState);

            var login = await _authenticationService.Login(user.Email, user.Password);

            if (login)
            {
                var response = await _authenticationService.GenerateJwt(user.Email);

                return new LoginResponseViewModel
                {
                    AccessToken = response.AccessToken,
                    ExpiresIn = response.ExpiresIn
                };

			}

            return CustomizeResponse();
        }

    }
}
