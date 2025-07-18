using DannyKeyboard.Application.DTOs.User;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DannyKeyboard.Application.Features.User.Commands
{
    public class DeleteUserCommand : IRequest<(bool,string)>
    {
        public DeleteUserRequestDto Dto { get; }

        public DeleteUserCommand(DeleteUserRequestDto dto)
        {
            Dto = dto;
        }
    }
}
