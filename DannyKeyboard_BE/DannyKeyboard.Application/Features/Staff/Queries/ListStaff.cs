using DannyKeyboard.Application.DTOs.Staff;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DannyKeyboard.Application.Features.Staff.Queries
{
    public record ListStaffQuery : IRequest<ListStaffResponseDto>;
    public class ListStaffHandler : IRequestHandler<ListStaffQuery, ListStaffResponseDto>
    {
        private readonly IUnitOfWork _unitOfWork;

        public ListStaffHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<ListStaffResponseDto> Handle(ListStaffQuery request, CancellationToken cancellationToken)
        {
            var list = (await _unitOfWork.StaffRepo.GetAll()).ToList();

            return new ListStaffResponseDto
            {
                StaffCount = list.Count,
                StaffList = list,
            };
        }
    }
}
