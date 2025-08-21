using DannyKeyboard.Application.DTOs.Brand;
using DannyKeyboard.Application.DTOs.Shift;
using DannyKeyboard.Application.Features.Brand.Commands;
using DannyKeyboard.Application.Features.Brand.Queries;
using DannyKeyboard.Application.Features.Shift.Commands;
using DannyKeyboard.Application.Features.Shift.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DannyKeyboard.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandController : ControllerBase
    {
        private readonly IMediator _mediator;
        public BrandController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetList()
        {
            var result = await _mediator.Send(new GetAllBrandQuery());
            return Ok(result);
        }

        [HttpGet("{brandId}")]
        public async Task<IActionResult> GetDetailOfBrand(int brandId)
        {
            var result = await _mediator.Send(new BrandDetailQuery(brandId));
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateBrand([FromBody] CreateBrandDto dto)
        {
            var result = await _mediator.Send(new CreateBrandCommand(dto));
            return Ok(result);
        }

        [HttpPut("{brandId}")]
        public async Task<IActionResult> UpdateBrand(int brandId, [FromBody] UpdateBrandDto dto)
        {
            if (brandId != dto.BrandId)
            {
                return BadRequest();
            }

            var result = await _mediator.Send(new UpdateBrandCommand(dto));
            return Ok(new
            {
                IsUpdate = result.Item1,
                Message = result.Item2
            });
        }

        [HttpDelete("{brandId}")]
        public async Task<IActionResult> DeleteBrand(int brandId)
        {
            var result = await _mediator.Send(new DeleteBrandCommand(brandId));
            return Ok(new
            {
                IsDelete = result.Item1,
                Message = result.Item2
            });
        }
    }
}
