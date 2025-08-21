using DannyKeyboard.Application.DTOs.Brand;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DannyKeyboard.Application.Features.Brand.Commands
{
    public class CreateBrandCommand : IRequest<bool>
    {
        public CreateBrandDto Dto { get; set; }
        public CreateBrandCommand(CreateBrandDto dto)
        {
            Dto = dto;
        }
    }
}
