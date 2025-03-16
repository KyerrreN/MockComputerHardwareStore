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

        /// <summary>
        /// Adds a new user to consume our API
        /// </summary>
        /// <param name="userForRegistration"></param>
        /// <returns></returns>
        /// <response code="201">If a user was created succesfully</response>
        /// <response code="400">If user information is null</response>
        /// <response code="422">If user information is invalid</response>
        [HttpPost]
        [ServiceFilter(typeof(BindingValidationFilterAttribute))]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(422)]
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

        /// <summary>
        /// Authenticates a user based on his credentials
        /// </summary>
        /// <param name="user"></param>
        /// <returns>Access and refresh tokens</returns>
        /// <response code="200">Returns access and refresh tokens for a specified user</response>\
        /// <response code="401">If a user with provided credentials does not exist</response>
        /// <response code="422">If user information is invalid</response>
        [HttpPost("login")]
        [ServiceFilter(typeof(BindingValidationFilterAttribute))]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(422)]
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
