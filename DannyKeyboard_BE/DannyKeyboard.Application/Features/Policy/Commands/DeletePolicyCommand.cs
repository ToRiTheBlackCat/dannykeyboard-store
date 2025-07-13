using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DannyKeyboard.Application.Features.Policy.Commands
{
    public class DeletePolicyCommand : IRequest<bool>
    {
        public int Id { get; }

        public DeletePolicyCommand(int id)
        {
            Id = id;
        }
    }
}
