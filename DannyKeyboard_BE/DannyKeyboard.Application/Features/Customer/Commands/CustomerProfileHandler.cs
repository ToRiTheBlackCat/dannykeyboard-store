using DannyKeyboard.Application.DTOs.Customer;
using MediatR;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DannyKeyboard.Application.Features.Customer.Commands
{
    public class CustomerProfileHandler : IRequestHandler<CustomerProfileCommand, CustomerProfileResponseDto>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CustomerProfileHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<CustomerProfileResponseDto> Handle(CustomerProfileCommand request, CancellationToken cancellationToken)
        {
            var response = new CustomerProfileResponseDto();
            try
            {
                await _unitOfWork.BeginTransactionAsync();

                //Find existed Customer
                var foundCustomer = await _unitOfWork.UserRepo.GetCustomerByUserId(request.Dto.CustomerId);
                if (foundCustomer == null)
                {
                    return response;
                }

                //Map data to response
                response.CustomerId = foundCustomer.Customer.CustomerId.Trim();
                response.Email = foundCustomer.Email;
                response.RoleId = foundCustomer.RoleId;
                response.RoleName = foundCustomer.Role.RoleName;
                response.FullName = foundCustomer.Customer.FullName;
                response.Address = foundCustomer.Customer.Address;
                response.Phone = foundCustomer.Customer.Phone;
                response.Dob = foundCustomer.Customer.Dob;

                return response;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                await _unitOfWork.RollbackTransactionAsync();
                return response;
            }
        }
    }
}
