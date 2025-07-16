using DannyKeyboard.Application.DTOs.Policy;
using DannyKeyboard.Application.DTOs.Role;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DannyKeyboard.Application.Features.Role.Commands
{
    public class UpdateRoleCommand : IRequest<bool>
    {
        public UpdateRoleDto Dto { get; set; }

        public UpdateRoleCommand(UpdateRoleDto dto)
        {
            Dto = dto;
        }
    }
}
