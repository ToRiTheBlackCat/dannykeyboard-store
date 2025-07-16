using DannyKeyboard.Application.DTOs.Customer;
using DannyKeyboard.Application.DTOs.Staff;
using DannyKeyboard.Application.Mappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DannyKeyboard.Application.Features.Staff.Queries
{
    public record StaffProfileQuery(StaffProfileRequestDto Dto)
        : IRequest<StaffProfileResponseDto>;
    public class StaffProfileHandler : IRequestHandler<StaffProfileQuery, StaffProfileResponseDto>
    {
        private readonly IUnitOfWork _unitOfWork;

        public StaffProfileHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<StaffProfileResponseDto> Handle(StaffProfileQuery request, CancellationToken cancellationToken)
        {
            var response = new StaffProfileResponseDto();
            try
            {
                //Find existed Staff
                var foundStaff = await _unitOfWork.UserRepo.GetStaffByUserId(request.Dto.StaffId);
                if (foundStaff == null)
                {
                    return response;
                }

                //Map data to response
                response = StaffMapper.ToStaffProfileResponseDto(foundStaff);
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
