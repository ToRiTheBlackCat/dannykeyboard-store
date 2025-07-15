using DannyKeyboard.Application.DTOs.Customer;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DannyKeyboard.Application.Features.Customer.Commands
{
    public class UpdateCustomerProfileCommand : IRequest<UpdateCustomerProfileDto>
    {
        public UpdateCustomerProfileDto Dto { get; set; }
        public UpdateCustomerProfileCommand(UpdateCustomerProfileDto dto)
        {
            Dto = dto;
        }
    }
}
