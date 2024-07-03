using Assistt.Application.Commands;
using Assistt.Application.Services.Auth;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Assistt.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private IMediator _mediator;
        private IAuthenticationService _authenticationService;

        public AuthController(IMediator mediator, IAuthenticationService authenticationService)
        {
            _mediator = mediator;
            _authenticationService = authenticationService;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(UserCommands.UserLogin userLogin)
        {
            var token = await _mediator.Send(userLogin);
            var refreshToken = _authenticationService.GenerateRefreshTokenAsync(token.UserId);
            return Ok(new { token = token.Token, refreshToken = refreshToken.Token});
        }


        [HttpPost]
        [Route("refresh-token")]
        public async Task<IActionResult> RefreshToken(Sample data)
        {
            var accessToken = _authenticationService.RefreshTokenAsync(data.token);
            return Ok(new { token = accessToken });
        }
    }


    public class Sample
    {
        public string token { get; set; }
    }
}
