using DannyKeyboard.Application.DTOs.AboutUs;
using DannyKeyboard.Application.DTOs.Policy;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DannyKeyboard.Application.Features.Policy.Commands
{
    public class CreatePolicyCommand : IRequest<bool>
    {
        public CreatePolicyDto Dto { get; set; }
        public CreatePolicyCommand(CreatePolicyDto dto)
        {
            Dto = dto;
        }
    }
}
