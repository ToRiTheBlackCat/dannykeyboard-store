using DannyKeyboard.Application.DTOs.Customer;
using DannyKeyboard.Application.DTOs.Staff;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DannyKeyboard.Application.Features.Staff.Commands
{
    public class UpdateStaffProfileCommand : IRequest<UpdateStaffProfileDto>
    {
        public UpdateStaffProfileDto Dto { get; set; }
        public UpdateStaffProfileCommand(UpdateStaffProfileDto dto)
        {
            Dto = dto;
        }
    }
}
