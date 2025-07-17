using DannyKeyboard.Application.DTOs.Staff;
using DannyKeyboard.Application.DTOs.User;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DannyKeyboard.Application.Features.Staff.Commands
{
    public class StaffSignUpCommand : IRequest<(bool, string)>
    {
        public SignUpStaffRequestDto Dto { get; set; }
        public StaffSignUpCommand(SignUpStaffRequestDto dto)
        {
            Dto = dto;
        }
    }
}
