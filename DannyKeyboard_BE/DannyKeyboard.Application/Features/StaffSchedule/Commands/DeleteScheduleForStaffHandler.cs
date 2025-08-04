using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DannyKeyboard.Application.Features.StaffSchedule.Commands
{
    public class DeleteScheduleForStaffHandler : IRequestHandler<DeleteScheduleForStaffCommand, (bool, string)>
    {
        private readonly IUnitOfWork _unitOfWork;
        private static string SUCCESS = "Delete schedule successfully";
        private static string ERROR = "Error when delete schedule";
        private static string NOTFOUND = "Delete fail. Not found any schedule with that id";
        public DeleteScheduleForStaffHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<(bool, string)> Handle(DeleteScheduleForStaffCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();

                //Find existed Schedule
                var foundSchedule = await _unitOfWork.StaffScheduleRepo.GetOne(request.SheduleId);
                if (foundSchedule == null)
                {
                    return (false, NOTFOUND);
                }

                _unitOfWork.StaffScheduleRepo.Delete(foundSchedule);
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
