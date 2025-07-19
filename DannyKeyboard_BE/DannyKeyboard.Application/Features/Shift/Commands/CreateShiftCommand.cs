using DannyKeyboard.Application.DTOs.Shift;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DannyKeyboard.Application.Features.Shift.Commands
{
    public class CreateShiftCommand : IRequest<bool>
    {
        public CreateShiftDto Dto  { get; set; }
        public CreateShiftCommand(CreateShiftDto dto)
        {
            Dto = dto;
        }
    }
}
