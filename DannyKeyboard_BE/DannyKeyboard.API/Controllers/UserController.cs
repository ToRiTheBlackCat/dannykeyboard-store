using DannyKeyboard.Application.DTOs.Password;
using DannyKeyboard.Application.DTOs.User;
using DannyKeyboard.Application.Features.OTP.Commands;
using DannyKeyboard.Application.Features.User.Commands;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DannyKeyboard.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;
        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("customer/send-otp")]
        public async Task<IActionResult> SendOtpToEmail([FromBody] OtpRequestDto request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _mediator.Send(new SendOTPCommand(request.Email));

            return result.Item1
                ? Ok(new { result.Item1, result.Item2 })
                : BadRequest(new { result.Item1, result.Item2 });
        }
    }
}
