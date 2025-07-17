using DannyKeyboard.Application.DTOs.Password;
using DannyKeyboard.Application.DTOs.User;
using DannyKeyboard.Application.Features.Customer.Queries;
using DannyKeyboard.Application.Features.OTP.Commands;
using DannyKeyboard.Application.Features.Policy.Queries;
using DannyKeyboard.Application.Features.Staff.Queries;
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

        [HttpPost("signup/send-otp")]
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
        [HttpGet("/api/staff")]
        public async Task<IActionResult> GetListStaff()
        {
            var result = await _mediator.Send(new ListStaffQuery());

            return Ok(result);
        }
        [HttpGet("/api/customer")]
        public async Task<IActionResult> GetListCustomer()
        {
            var result = await _mediator.Send(new ListCustomerQuery());

            return Ok(result);
        }
    }
}
