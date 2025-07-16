using DannyKeyboard.Application.DTOs.AboutUs;
using DannyKeyboard.Application.Features.AboutUs.Queries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DannyKeyboard.Application.Features.Role.Queries
{
    public record GetAllRoleQuery : IRequest<List<Domain.Entities.Role>>;
    public class GetAllRoleHandler : IRequestHandler<GetAllRoleQuery, List<Domain.Entities.Role>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllRoleHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<Domain.Entities.Role>> Handle(GetAllRoleQuery request, CancellationToken cancellationToken)
        {
            var list = (await _unitOfWork.RoleRepo.GetAll()).ToList();

            return list;
        }
    }
}
