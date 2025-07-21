using DannyKeyboard.Application.DTOs.Shift;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DannyKeyboard.Application.Features.Shift.Queries
{
    public record ShiftDetailQuery(int id) : IRequest<Domain.Entities.Shift?>;
    public class ShiftDetailHandler : IRequestHandler<ShiftDetailQuery, Domain.Entities.Shift?>
    {
        private readonly IUnitOfWork _unitOfWork;

        public ShiftDetailHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Domain.Entities.Shift?> Handle(ShiftDetailQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var response = await _unitOfWork.ShiftRepo.GetOne(request.id);

                return response;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return new Domain.Entities.Shift();
            }
        }
    }
}
