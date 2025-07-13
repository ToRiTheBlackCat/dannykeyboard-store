using DannyKeyboard.Application.DTOs.Policy;
using DannyKeyboard.Application.DTOs.Token;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DannyKeyboard.Application.Features.Token.Commands
{
    public class GenerateTokenCommand : IRequest<RefreshTokenResponseDto>
    {
        public RefreshTokenRequestDto Dto { get; set; }
        public GenerateTokenCommand(RefreshTokenRequestDto dto)
        {
            Dto = dto;
        }
    }
}
