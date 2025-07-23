using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DannyKeyboard.Application.Features.StaffSchedule.Queries
{
    public record StaffScheduleDetailQuery(int scheduleId) : IRequest<Domain.Entities.StaffSchedule?>;
    public class StaffScheduleDetailHandler : IRequestHandler<StaffScheduleDetailQuery, Domain.Entities.StaffSchedule?>
    {
        private readonly IUnitOfWork _unitOfWork;

        public StaffScheduleDetailHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Domain.Entities.StaffSchedule?> Handle(StaffScheduleDetailQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var response = await _unitOfWork.StaffScheduleRepo.GetOne(request.scheduleId);

                return response;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return new Domain.Entities.StaffSchedule();
            }
        }
    }
}
