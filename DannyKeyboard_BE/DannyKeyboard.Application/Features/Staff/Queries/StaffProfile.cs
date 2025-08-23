using DannyKeyboard.Application.Common;
using DannyKeyboard.Application.DTOs.Customer;
using DannyKeyboard.Application.DTOs.Staff;
using DannyKeyboard.Application.Mappers;
using MediatR;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DannyKeyboard.Application.Features.Staff.Queries
{
    public record StaffProfileQuery(StaffProfileRequestDto Dto)
        : IRequest<StaffProfileResponseDto>;
    public class StaffProfileHandler : IRequestHandler<StaffProfileQuery, StaffProfileResponseDto?>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMemoryCache _cache;

        public StaffProfileHandler(IUnitOfWork unitOfWork, IMemoryCache cache)
        {
            _unitOfWork = unitOfWork;
            _cache = cache;
        }

        public async Task<StaffProfileResponseDto?> Handle(StaffProfileQuery request, CancellationToken cancellationToken)
        {
            var response = new StaffProfileResponseDto();
            try
            {
                if (_cache.TryGetValue(ConstantString.STAFFPROFILE_CACHE + request.Dto.StaffId, out StaffProfileResponseDto? cachedStaff))
                {
                    return cachedStaff;
                }

                //Find existed Staff
                var foundStaff = await _unitOfWork.UserRepo.GetStaffByUserId(request.Dto.StaffId);
                if (foundStaff == null)
                {
                    return response;
                }

                //Map data to response
                response = StaffMapper.ToStaffProfileResponseDto(foundStaff);
                _cache.Set(ConstantString.STAFFPROFILE_CACHE + request.Dto.StaffId, response, TimeSpan.FromDays(30));

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
