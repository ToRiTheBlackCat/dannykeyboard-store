using DannyKeyboard.Application.Common;
using DannyKeyboard.Application.DTOs.Staff;
using MediatR;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DannyKeyboard.Application.Features.Staff.Queries
{
    public record ListStaffQuery : IRequest<ListStaffResponseDto>;
    public class ListStaffHandler : IRequestHandler<ListStaffQuery, ListStaffResponseDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMemoryCache _cache;


        public ListStaffHandler(IUnitOfWork unitOfWork, IMemoryCache cache)
        {
            _unitOfWork = unitOfWork;
            _cache = cache;
        }

        public async Task<ListStaffResponseDto> Handle(ListStaffQuery request, CancellationToken cancellationToken)
        {
            if (_cache.TryGetValue(ConstantString.STAFF_CACHE, out List<Domain.Entities.Staff>? cachedList))
            {
                return new ListStaffResponseDto
                {
                    StaffCount = cachedList?.Count ?? 0,
                    StaffList = cachedList ?? new(),
                };
            }

            var list = (await _unitOfWork.StaffRepo.GetAll()).ToList();
            _cache.Set(ConstantString.STAFF_CACHE, list, TimeSpan.FromDays(7));

            return new ListStaffResponseDto
            {
                StaffCount = list.Count,
                StaffList = list,
            };
        }
    }
}
