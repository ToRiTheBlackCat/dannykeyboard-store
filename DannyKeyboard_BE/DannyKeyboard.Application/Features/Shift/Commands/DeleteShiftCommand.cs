using DannyKeyboard.Application.DTOs.Shift;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DannyKeyboard.Application.Features.Shift.Commands
{
    public class DeleteShiftCommand : IRequest<(bool,string)>
    {
        public int ShiftId { get; set; }
        public DeleteShiftCommand(int shiftId)
        {
            ShiftId = shiftId;
        }
    }
}
