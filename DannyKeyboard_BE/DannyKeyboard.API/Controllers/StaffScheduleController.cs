using DannyKeyboard.Application.DTOs.StaffSchedule;
using DannyKeyboard.Application.Features.Shift.Queries;
using DannyKeyboard.Application.Features.StaffSchedule.Commands;
using DannyKeyboard.Application.Features.StaffSchedule.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DannyKeyboard.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StaffScheduleController : ControllerBase
    {
        private readonly IMediator _mediator;
        public StaffScheduleController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetList()
        {
            var result = await _mediator.Send(new GetAllStaffScheduleQuery());
            return Ok(result);
        }

        [HttpGet("shift/{scheduleId}")]
        public async Task<IActionResult> GetDetailOfStaffSchedule(int scheduleId)
        {
            var result = await _mediator.Send(new StaffScheduleDetailQuery(scheduleId));
            return Ok(result);
        }

        [HttpGet("staff/{staffId}")]
        public async Task<IActionResult> GetStaffScheduleOfStaff(string staffId)
        {
            var result = await _mediator.Send(new StaffScheduleDetailOfStaffQuery(staffId));
            return Ok(result);
        }

        [HttpPost("{staffId}")]
        public async Task<IActionResult> CreateScheduleOfStaff(string staffId, [FromBody] CreateScheduleOfStaffDto dto)
        {
            if (staffId != dto.StaffId)
            {
                return BadRequest("Not match StaffId. Try valid input!");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _mediator.Send(new CreateScheduleForStaffCommand(staffId, dto));
            return result.Item1
                ? Ok(new
                {
                    IsCreated = result.Item1,
                    Message = result.Item2
                })
                : BadRequest(new
                {
                    IsCreated = result.Item1,
                    Message = result.Item2
                });
        }
    }
}
