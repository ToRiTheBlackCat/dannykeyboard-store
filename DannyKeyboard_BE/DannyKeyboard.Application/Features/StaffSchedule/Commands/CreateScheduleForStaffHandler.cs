using DannyKeyboard.Application.Mappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DannyKeyboard.Application.Features.StaffSchedule.Commands
{
    public class CreateScheduleForStaffHandler : IRequestHandler<CreateScheduleForStaffCommand, (bool, string)>
    {
        private readonly IUnitOfWork _unitOfWork;
        private static string SUCCESS = "Create schedule for staff successfully";
        private static string ERROR = "Error when create schedule for staff";
        private static string NOTEXISTSTAFF = "Not exist any staff with that StaffId";
        private static string NOTEXISTSHIFT = "Not exist any shift with that ShiftId";

        public CreateScheduleForStaffHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<(bool, string)> Handle(CreateScheduleForStaffCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();

                //Find existed staff
                var foundStaff = await _unitOfWork.StaffRepo.GetOne(request.StaffId);
                if (foundStaff == null)
                {
                    return (false, NOTEXISTSTAFF);
                }

                //Find existed shift
                var foundShift = await _unitOfWork.ShiftRepo.GetOne(request.Dto.ShiftId);
                if (foundShift == null)
                {
                    return (false, NOTEXISTSHIFT);
                }

                //Mapping data
                var newStaffSchedule = StaffScheduleMapper.ToStaffSchedule(request.Dto);
                await _unitOfWork.StaffScheduleRepo.Insert(newStaffSchedule);

                await _unitOfWork.CommitTransactionAsync();
                return (true, SUCCESS);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                await _unitOfWork.RollbackTransactionAsync();
                return (false, ERROR);
            }
        }
    }
}
