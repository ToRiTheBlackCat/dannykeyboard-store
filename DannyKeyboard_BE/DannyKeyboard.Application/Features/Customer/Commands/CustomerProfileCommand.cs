using DannyKeyboard.Application.DTOs.Customer;
using MediatR;

namespace DannyKeyboard.Application.Features.Customer.Commands
{
    public class CustomerProfileCommand : IRequest<CustomerProfileResponseDto>
    {
        public CustomerProfileRequestDto Dto { get; set; }
        public CustomerProfileCommand(CustomerProfileRequestDto dto)
        {
            Dto = dto;
        }
    }
}
