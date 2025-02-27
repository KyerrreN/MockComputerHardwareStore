using ComputerHardwareStore.Presentation.ActionFilters;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Shared.DataTransferObjects;

namespace ComputerHardwareStore.Presentation.Controllers
{
    [ApiController]
    [Route("api/authentication")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IServiceManager _service;
        private readonly IValidator<UserForRegistrationDto> _userForRegistrationValidator;
        private readonly IValidator<UserForAuthenticationDto> _userForAuthenticationValidator;
        public AuthenticationController(IServiceManager service, IValidator<UserForRegistrationDto> userForRegistrationValidator, IValidator<UserForAuthenticationDto> userForAuthenticationValidator)
        {
            _service = service;
            _userForRegistrationValidator = userForRegistrationValidator;
            _userForAuthenticationValidator = userForAuthenticationValidator;
        }

        [HttpPost]
        [ServiceFilter(typeof(BindingValidationFilterAttribute))]
        public async Task<IActionResult> RegisterUser([FromBody] UserForRegistrationDto userForRegistration)
        {
            _userForRegistrationValidator.ValidateAndThrow(userForRegistration);

            var result = await _service.AuthenticationService.RegisterUser(userForRegistration);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.TryAddModelError(error.Code, error.Description);
                }

                return BadRequest(ModelState);
            }

            return StatusCode(201);
        }

        [HttpPost("login")]
        [ServiceFilter(typeof(BindingValidationFilterAttribute))]
        public async Task<IActionResult> Authenticate([FromBody] UserForAuthenticationDto user)
        {
            _userForAuthenticationValidator.ValidateAndThrow(user);

            if (!await _service.AuthenticationService.ValidateUser(user))
            {
                return Unauthorized();
            }

            var tokenDto = await _service.AuthenticationService.CreateToken(populateExp: true);

            return Ok(tokenDto);
        }
    }
}
