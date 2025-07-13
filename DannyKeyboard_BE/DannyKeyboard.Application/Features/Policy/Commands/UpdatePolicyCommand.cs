using DannyKeyboard.Application.DTOs.Policy;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DannyKeyboard.Application.Features.Policy.Commands
{
    public class UpdatePolicyCommand : IRequest<bool>
    {
        public UpdatePolicyDto Dto { get; set; }

        public UpdatePolicyCommand(UpdatePolicyDto dto)
        {
            Dto = dto;
        }
    }
}
