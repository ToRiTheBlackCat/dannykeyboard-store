using DannyKeyboard.Application.DTOs.Shift;
using DannyKeyboard.Application.Features.Policy.Queries;
using DannyKeyboard.Application.Features.Shift.Commands;
using DannyKeyboard.Application.Features.Shift.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DannyKeyboard.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShiftController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ShiftController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetList()
        {
            var result = await _mediator.Send(new GetAllShiftQuery());
            return Ok(result);
        }

        [HttpPost("{shiftId}")]
        public async Task<IActionResult> GetDetailOfShift([FromBody] ShiftDetailRequestDto dto)
        {
            var result = await _mediator.Send(new GetAllShiftQuery());
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateShift([FromBody] CreateShiftDto dto)
        {
            var result = await _mediator.Send(new CreateShiftCommand(dto));
            return Ok(result);
        }
    }
}
