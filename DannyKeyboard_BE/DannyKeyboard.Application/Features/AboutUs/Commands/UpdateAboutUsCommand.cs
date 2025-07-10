using DannyKeyboard.Application.DTOs.AboutUs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DannyKeyboard.Application.Features.AboutUs.Commands
{
    public class UpdateAboutUsCommand : IRequest<bool>
    {
        public UpdateAboutUsDto Dto { get; set; }

        public UpdateAboutUsCommand(UpdateAboutUsDto dto)
        {
            Dto = dto;
        }
    }
}
