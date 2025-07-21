using DannyKeyboard.Application.DTOs.Shift;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DannyKeyboard.Application.Features.Shift.Commands
{
    public class UpdateShiftCommand : IRequest<(bool,string)>
    {
        public UpdateShiftDto Dto { get; set; }
        public UpdateShiftCommand(UpdateShiftDto dto)
        {
            Dto = dto;
        }
    }
}
