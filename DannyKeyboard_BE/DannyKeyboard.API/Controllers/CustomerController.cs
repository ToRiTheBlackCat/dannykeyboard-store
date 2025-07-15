using DannyKeyboard.Application.DTOs.Customer;
using DannyKeyboard.Application.DTOs.User;
using DannyKeyboard.Application.Features.Customer.Commands;
using DannyKeyboard.Application.Features.Customer.Queries;
using DannyKeyboard.Application.Features.OTP.Commands;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DannyKeyboard.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly IMediator _mediator;
        public CustomerController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("profile")]
        public async Task<IActionResult> GetCustomerProfile([FromBody] CustomerProfileRequestDto request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _mediator.Send(new CustomerProfileQuery(request));
            
            return Ok(result);
        }

        [HttpPut("update-profile")]
        public async Task<IActionResult> UpdateCustomerProfile([FromBody] UpdateCustomerProfileDto request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _mediator.Send(new UpdateCustomerProfileCommand(request));

            return Ok(result);
        }
    }
}
