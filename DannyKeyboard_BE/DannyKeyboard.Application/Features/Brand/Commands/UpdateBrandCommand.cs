using DannyKeyboard.Application.DTOs.Brand;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DannyKeyboard.Application.Features.Brand.Commands
{
    public class UpdateBrandCommand : IRequest<(bool,string)>
    {
        public UpdateBrandDto Dto { get; set; }
        public UpdateBrandCommand(UpdateBrandDto dto)
        {
            Dto = dto;
        }
    }
}
