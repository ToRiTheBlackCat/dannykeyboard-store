using DannyKeyboard.Application.DTOs.Customer;
using DannyKeyboard.Application.DTOs.Staff;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DannyKeyboard.Application.Features.Staff.Commands
{
    public class UpdateStaffProfileHandler : IRequestHandler<UpdateStaffProfileCommand, UpdateStaffProfileDto>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateStaffProfileHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<UpdateStaffProfileDto> Handle(UpdateStaffProfileCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();

                //Find existed user
                var foundStaff = await _unitOfWork.UserRepo.GetStaffByUserId(request.Dto.StaffId);
                if (foundStaff == null)
                {
                    return new();
                }

                //Update data to found Customer
                foundStaff.Staff.FullName = request.Dto.FullName;
                foundStaff.Staff.Address = request.Dto.Address;
                foundStaff.Staff.Phone = request.Dto.Phone;
                foundStaff.Staff.Dob = request.Dto.Dob;

                _unitOfWork.StaffRepo.UpdateStaff(foundStaff.Staff);
                await _unitOfWork.CommitTransactionAsync();

                var updatedStaff = await _unitOfWork.UserRepo.GetCustomerByUserId(foundStaff.UserId);
                if (updatedStaff == null)
                {
                    return new();
                }

                //Add data to response
                var response = new UpdateStaffProfileDto
                {
                    StaffId = updatedStaff.Staff.StaffId.Trim(),
                    FullName = updatedStaff.Staff.FullName,
                    Address = updatedStaff.Staff.Address,
                    Phone = updatedStaff.Staff.Phone,
                    Dob = updatedStaff.Staff.Dob
                };

                return response;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                await _unitOfWork.RollbackTransactionAsync();
                return new();
            }
        }
    }
}
