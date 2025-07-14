using DannyKeyboard.Application.DTOs.Password;
using DannyKeyboard.Application.DTOs.Token;
using DannyKeyboard.Application.DTOs.User;
using DannyKeyboard.Application.Features.Password.Commands;
using DannyKeyboard.Application.Features.Token.Commands;
using DannyKeyboard.Application.Features.User.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace DannyKeyboard.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;
        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _mediator.Send(new LoginCommand(dto));
            if (!result.IsAuthenticated)
                return NotFound(result);

            return Ok(result);
        }

        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequestDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _mediator.Send(new GenerateTokenCommand(dto));

            return Ok(result);
        }

        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword([EmailAddress] string email)
        {
            var result = await _mediator.Send(new ForgotPasswordCommand(email));

            return Ok(result);
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] RequestResetPassword request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _mediator.Send(new ResetPasswordCommand(request));

            return Ok(new
            {
                result.Item1,
                result.Item2
            });
        }
    }
}
