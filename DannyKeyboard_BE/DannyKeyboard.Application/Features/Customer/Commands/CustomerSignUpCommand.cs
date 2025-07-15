using DannyKeyboard.Application.DTOs.User;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DannyKeyboard.Application.Features.Customer.Commands
{
    public class CustomerSignUpCommand : IRequest<(bool,string)>
    {
        public CustomerSignUpRequestDto Dto { get; set; }
        public CustomerSignUpCommand(CustomerSignUpRequestDto dto)
        {
            Dto = dto;
        }
    }
}
