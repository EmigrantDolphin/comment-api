using System.Security.Claims;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;

using Application.Models;
using Application.Services;
using CommentApi.Services;
using CommentApi.ViewModels;
using CommentApi.ViewModels.User;

namespace CommentApi.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly IAuthenticationService _authenticationService;
        private readonly IUserService _userService;

        public UserController(
            ILogger<UserController> logger,
            IAuthenticationService authService,
            IUserService userService
            )
        {
            _logger = logger;
            _userService = userService;
            _authenticationService = authService;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("user/login")]
        public async Task<IActionResult> Login([FromBody] LoginModel loginModel)
        {
            try
            {
                var user = await _userService.GetUserByUsernameAndPasswordAsync(loginModel.Username, loginModel.Password);

                var claims = new []
                {
                    new Claim(ClaimTypes.Name, loginModel.Username),
                    new Claim("UserId", value: user.UserId.ToString())
                };

                var authResult = _authenticationService.GenerateAccessToken(loginModel.Username, claims, DateTime.Now);
                var response = new LoginResponseModel
                {
                    UserId = user.UserId,
                    Username = user.Username,
                    Token = authResult
                };

                return Ok(response);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(new ErrorModel(ex.Message));
            }
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("user")]
        public async Task<IActionResult> CreateUser([FromBody] UserRegistrationModel loginModel)
        {
            try
            {
                var userCreateModel = new UserCreateModel
                {
                    Username = loginModel.Username,
                    Password = loginModel.Password,
                    FullName = loginModel.FullName
                };

                await _userService.CreateUserAsync(userCreateModel);
                return Ok();
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(new ErrorModel(ex.Message));
            }
        }

    }
}
