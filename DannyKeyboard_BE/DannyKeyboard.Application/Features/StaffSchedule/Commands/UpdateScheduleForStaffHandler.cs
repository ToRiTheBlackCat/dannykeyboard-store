using DannyKeyboard.Application.Mappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DannyKeyboard.Application.Features.StaffSchedule.Commands
{
    public class UpdateScheduleForStaffHandler : IRequestHandler<UpdateScheduleForStaffCommand, (bool, string)>
    {
        private readonly IUnitOfWork _unitOfWork;
        private static string SUCCESS = "Update schedule for staff successfully";
        private static string ERROR = "Error when update schedule for staff";
        private static string NOTEXISTSTAFF = "Not exist any staff with that StaffId";
        private static string NOTEXISTSCHEDULE = "Not exist any schedule with that ScheduleId";

        public UpdateScheduleForStaffHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<(bool, string)> Handle(UpdateScheduleForStaffCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();

                //Find existed schedule
                var foundSchedule = await _unitOfWork.StaffScheduleRepo.GetOne(request.Dto.ScheduleId);
                if (foundSchedule == null)
                {
                    return (false, NOTEXISTSCHEDULE);
                }

                //Find existed staff
                var foundStaff = await _unitOfWork.StaffRepo.GetOne(request.Dto.StaffId);
                if (foundStaff == null)
                {
                    return (false, NOTEXISTSTAFF);
                }

                var updateSchedule = StaffScheduleMapper.ToStaffSchedule(request.Dto, foundSchedule);

                _unitOfWork.StaffScheduleRepo.Update(updateSchedule);
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
