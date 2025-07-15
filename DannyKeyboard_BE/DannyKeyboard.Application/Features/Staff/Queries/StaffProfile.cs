using DannyKeyboard.Application.DTOs.Customer;
using DannyKeyboard.Application.DTOs.Staff;
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
                response.StaffId = foundStaff.Staff.StaffId.Trim();
                response.Email = foundStaff.Email;
                response.RoleId = foundStaff.RoleId;
                response.RoleName = foundStaff.Role.RoleName;
                response.FullName = foundStaff.Staff.FullName;
                response.Address = foundStaff.Staff.Address;
                response.Phone = foundStaff.Staff.Phone;
                response.Dob = foundStaff.Staff.Dob;

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
