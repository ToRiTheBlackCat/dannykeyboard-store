using DannyKeyboard.Application.DTOs.AboutUs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DannyKeyboard.Application.Features.AboutUs.Commands
{
    public class CreateAboutUsCommand : IRequest<bool>
    {
        public CreateAboutUsDto Dto { get; set; }

        public CreateAboutUsCommand(CreateAboutUsDto dto)
        {
            Dto = dto;
        }
    }
}
