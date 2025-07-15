using DannyKeyboard.Application.DTOs.Customer;
using DannyKeyboard.Application.DTOs.Staff;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DannyKeyboard.Application.Features.Customer.Queries
{
    public record ListCustomerQuery : IRequest<ListCustomerResponseDto>;
    public class ListCustomerHandler : IRequestHandler<ListCustomerQuery, ListCustomerResponseDto>
    {
        private readonly IUnitOfWork _unitOfWork;

        public ListCustomerHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<ListCustomerResponseDto> Handle(ListCustomerQuery request, CancellationToken cancellationToken)
        {
            var list = (await _unitOfWork.CustomerRepo.GetAll()).ToList();

            return new ListCustomerResponseDto
            {
                CustomerCount = list.Count,
                CustomerList = list,
            };
        }
    }
}
