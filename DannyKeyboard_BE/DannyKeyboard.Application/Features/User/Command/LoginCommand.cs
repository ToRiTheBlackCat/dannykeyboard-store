using DannyKeyboard.Application.DTOs.AboutUs;
using DannyKeyboard.Application.DTOs.User;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DannyKeyboard.Application.Features.User.Command
{
    public class LoginCommand : IRequest<LoginResponseDto>
    {
        public LoginRequestDto Dto { get; set; }

        public LoginCommand(LoginRequestDto dto)
        {
            Dto = dto;
        }
    }
}
