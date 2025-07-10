using DannyKeyboard.Application.DTOs.AboutUs;
using DannyKeyboard.Application.Features.AboutUs.Commands;
using DannyKeyboard.Application.Features.AboutUs.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DannyKeyboard.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AboutUsController : ControllerBase
    {
        private readonly IMediator _mediator;
        public AboutUsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetList()
        {
            var result = await _mediator.Send(new GetAllAboutUsQuery());
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateAboutUsDto dto)
        {
            var result = await _mediator.Send(new CreateAboutUsCommand(dto));
            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateAboutUsDto dto)
        {
            var result = await _mediator.Send(new UpdateAboutUsCommand(dto));
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _mediator.Send(new DeleteAboutUsCommand(id));
            return Ok(result);
        }
    }
}
