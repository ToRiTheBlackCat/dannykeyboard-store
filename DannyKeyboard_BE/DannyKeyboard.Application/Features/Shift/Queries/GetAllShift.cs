using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DannyKeyboard.Application.Features.Shift.Queries
{
    public class GetAllShiftQuery : IRequest<List<Domain.Entities.Shift>>;
    public class GetAllShiftHandler : IRequestHandler<GetAllShiftQuery, List<Domain.Entities.Shift>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllShiftHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<Domain.Entities.Shift>> Handle(GetAllShiftQuery request, CancellationToken cancellationToken)
        {
            return (await _unitOfWork.ShiftRepo.GetAll()).ToList();
        }
    }
}
