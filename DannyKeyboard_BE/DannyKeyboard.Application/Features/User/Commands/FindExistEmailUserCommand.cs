using DannyKeyboard.Application.DTOs.User;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DannyKeyboard.Application.Features.User.Commands
{
    public class FindExistEmailUserCommand : IRequest<bool>
    {
        public string Email { get; set; }

        public FindExistEmailUserCommand(string email)
        {
            Email = email;
        }
    }
}
