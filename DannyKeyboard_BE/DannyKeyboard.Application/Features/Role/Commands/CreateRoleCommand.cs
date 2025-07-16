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
    public class CreateRoleCommand : IRequest<bool>
    {
        public CreateRoleDto Dto { get; set; }
        public CreateRoleCommand(CreateRoleDto dto)
        {
            Dto = dto;
        }
    }
}
