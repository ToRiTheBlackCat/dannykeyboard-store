using DannyKeyboard.Application.DTOs.Password;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DannyKeyboard.Application.Features.Password.Commands
{
    public class ResetPasswordCommand : IRequest<(bool, string)>
    {
        public RequestResetPasswordDto Dto {  get; set; }
        public ResetPasswordCommand(RequestResetPasswordDto dto)
        {
            Dto = dto;
        }
    }
}
