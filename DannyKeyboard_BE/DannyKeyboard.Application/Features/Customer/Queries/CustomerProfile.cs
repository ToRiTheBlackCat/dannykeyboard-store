using DannyKeyboard.Application.Common;
using DannyKeyboard.Application.DTOs.Customer;
using DannyKeyboard.Application.DTOs.Policy;
using DannyKeyboard.Application.Features.Customer.Commands;
using DannyKeyboard.Application.Features.Policy.Queries;
using MediatR;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DannyKeyboard.Application.Features.Customer.Queries
{
    public record CustomerProfileQuery(CustomerProfileRequestDto Dto)
        : IRequest<CustomerProfileResponseDto>;

    public class CustomerProfileHandler : IRequestHandler<CustomerProfileQuery, CustomerProfileResponseDto?>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMemoryCache _cache;

        public CustomerProfileHandler(IUnitOfWork unitOfWork, IMemoryCache cache)
        {
            _unitOfWork = unitOfWork;
            _cache = cache;
        }

        public async Task<CustomerProfileResponseDto?> Handle(CustomerProfileQuery request, CancellationToken cancellationToken)
        {
            var response = new CustomerProfileResponseDto();
            try
            {
                if (_cache.TryGetValue(ConstantString.CUSTOMERPROFILE_CACHE + request.Dto.CustomerId, out CustomerProfileResponseDto? cachedCustomer))
                {
                    return cachedCustomer;
                }

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

                _cache.Set(ConstantString.CUSTOMERPROFILE_CACHE + request.Dto.CustomerId, response, TimeSpan.FromDays(7));

                return response;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return response;
            }
        }

    }
}
