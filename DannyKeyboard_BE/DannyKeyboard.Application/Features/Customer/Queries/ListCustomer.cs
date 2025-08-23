using DannyKeyboard.Application.Common;
using DannyKeyboard.Application.DTOs.Customer;
using DannyKeyboard.Application.DTOs.Staff;
using MediatR;
using Microsoft.Extensions.Caching.Memory;
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
        private readonly IMemoryCache _cache;


        public ListCustomerHandler(IUnitOfWork unitOfWork, IMemoryCache cache)
        {
            _unitOfWork = unitOfWork;
            _cache = cache;
        }

        public async Task<ListCustomerResponseDto> Handle(ListCustomerQuery request, CancellationToken cancellationToken)
        {
            if (_cache.TryGetValue(ConstantString.CUSTOMER_CACHE, out List<Domain.Entities.Customer>? cachedList))
            {
                return new ListCustomerResponseDto
                {
                    CustomerCount = cachedList?.Count ?? 0,
                    CustomerList = cachedList ?? new(),
                };
            }

            var list = (await _unitOfWork.CustomerRepo.GetAll()).ToList();
            _cache.Set(ConstantString.CUSTOMER_CACHE, list, TimeSpan.FromDays(1));

            return new ListCustomerResponseDto
            {
                CustomerCount = list.Count,
                CustomerList = list,
            };
        }
    }
}
