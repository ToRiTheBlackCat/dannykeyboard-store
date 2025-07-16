using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DannyKeyboard.Application.Features.Role.Commands
{
    public class DeleteRoleCommand : IRequest<bool>
    {
        public int Id { get; }

        public DeleteRoleCommand(int id)
        {
            Id = id;
        }
    }
}
