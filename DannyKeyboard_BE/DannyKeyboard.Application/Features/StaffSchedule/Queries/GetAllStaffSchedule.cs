using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DannyKeyboard.Application.Features.StaffSchedule.Queries
{
    public class GetAllStaffScheduleQuery : IRequest<List<Domain.Entities.StaffSchedule>>;
    public class GetAllStaffScheduleHandler : IRequestHandler<GetAllStaffScheduleQuery, List<Domain.Entities.StaffSchedule>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllStaffScheduleHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<Domain.Entities.StaffSchedule>> Handle(GetAllStaffScheduleQuery request, CancellationToken cancellationToken)
        {
            return (await _unitOfWork.StaffScheduleRepo.GetAll()).ToList();
        }
    }
}
