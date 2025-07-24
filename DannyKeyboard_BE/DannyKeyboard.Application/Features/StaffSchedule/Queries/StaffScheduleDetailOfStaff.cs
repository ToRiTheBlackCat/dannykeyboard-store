using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DannyKeyboard.Application.Features.StaffSchedule.Queries
{
    public record StaffScheduleDetailOfStaffQuery(string staffId) : IRequest<List<Domain.Entities.StaffSchedule>>;
    public class StaffScheduleDetailOfStaffHandler : IRequestHandler<StaffScheduleDetailOfStaffQuery, List<Domain.Entities.StaffSchedule>?>
    {
        private readonly IUnitOfWork _unitOfWork;

        public StaffScheduleDetailOfStaffHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<Domain.Entities.StaffSchedule>?> Handle(StaffScheduleDetailOfStaffQuery request, CancellationToken cancellationToken)
        {
            var response = new List<Domain.Entities.StaffSchedule>();
            try
            {
                //Check existed staff
                var foundStaff = await _unitOfWork.StaffRepo.GetOne(request.staffId);
                if (foundStaff == null)
                {
                    return response;
                }

                response = await _unitOfWork.StaffScheduleRepo.GetScheduleOfStaff(request.staffId);
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
