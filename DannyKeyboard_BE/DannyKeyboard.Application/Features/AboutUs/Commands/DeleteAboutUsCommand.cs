using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DannyKeyboard.Application.Features.AboutUs.Commands
{
    public class DeleteAboutUsCommand : IRequest<bool>
    {
        public int Id { get; }

        public DeleteAboutUsCommand(int id)
        {
            Id = id;
        }
    }
}
