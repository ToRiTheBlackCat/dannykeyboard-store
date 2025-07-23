using DannyKeyboard.Application.Features.Shift.Queries;
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

        [HttpGet("{scheduleId}")]
        public async Task<IActionResult> GetDetailOfStaffSchedule(int scheduleId)
        {
            var result = await _mediator.Send(new StaffScheduleDetailQuery(scheduleId));
            return Ok(result);
        }
    }
}
