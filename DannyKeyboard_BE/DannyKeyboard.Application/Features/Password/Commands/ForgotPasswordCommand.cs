using DannyKeyboard.Application.DTOs.Password;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DannyKeyboard.Application.Features.Password.Commands
{
    public class ForgotPasswordCommand : IRequest<ResponseForgotPasswordDto>
    {
        public string Email { get; set; }
        public ForgotPasswordCommand(string email)
        {
            Email = email;
        }
    }
}
