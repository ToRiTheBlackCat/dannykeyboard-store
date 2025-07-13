using DannyKeyboard.Application.DTOs.AboutUs;
using DannyKeyboard.Application.DTOs.Policy;
using DannyKeyboard.Application.Features.AboutUs.Queries;
using DannyKeyboard.Application.Mappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DannyKeyboard.Application.Features.Policy.Queries
{
    public class GetAllPolicyQuery : IRequest<List<ListPolicyDto>>;

    public class GetAllAboutUsHandler : IRequestHandler<GetAllPolicyQuery, List<ListPolicyDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllAboutUsHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<ListPolicyDto>> Handle(GetAllPolicyQuery request, CancellationToken cancellationToken)
        {
            var list = (await _unitOfWork.PolicyRepo.GetAll()).ToList();
            if (list.Count == 0)
            {
                return new List<ListPolicyDto>();
            }

            return PolicyMapper.ToListPolicyDto(list);
        }
    }
}
