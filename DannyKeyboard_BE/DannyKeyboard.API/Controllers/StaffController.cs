using DannyKeyboard.Application.DTOs.Customer;
using DannyKeyboard.Application.DTOs.Staff;
using DannyKeyboard.Application.Features.Staff.Commands;
using DannyKeyboard.Application.Features.Staff.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DannyKeyboard.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StaffController : ControllerBase
    {
        private readonly IMediator _mediator;
        public StaffController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("profile")]
        public async Task<IActionResult> GetStaffProfile([FromBody] StaffProfileRequestDto request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _mediator.Send(new StaffProfileQuery(request));

            return Ok(result);
        }

        [HttpPut("update-profile")]
        public async Task<IActionResult> UpdateStaffProfile([FromBody] UpdateStaffProfileDto request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _mediator.Send(new UpdateStaffProfileCommand(request));

            return Ok(result);
        }
    }
}
