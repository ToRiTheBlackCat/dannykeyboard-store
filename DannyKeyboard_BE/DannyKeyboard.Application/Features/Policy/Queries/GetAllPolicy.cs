using DannyKeyboard.Application.Common;
using DannyKeyboard.Application.DTOs.AboutUs;
using DannyKeyboard.Application.DTOs.Policy;
using DannyKeyboard.Application.Features.AboutUs.Queries;
using DannyKeyboard.Application.Mappers;
using MediatR;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DannyKeyboard.Application.Features.Policy.Queries
{
    public class GetAllPolicyQuery : IRequest<List<ListPolicyDto>>;

    public class GetAllAboutUsHandler : IRequestHandler<GetAllPolicyQuery, List<ListPolicyDto>?>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMemoryCache _cache;


        public GetAllAboutUsHandler(IUnitOfWork unitOfWork, IMemoryCache cache)
        {
            _unitOfWork = unitOfWork;
            _cache = cache;
        }

        public async Task<List<ListPolicyDto>?> Handle(GetAllPolicyQuery request, CancellationToken cancellationToken)
        {
            if (_cache.TryGetValue(ConstantString.POLICY_CACHE, out List<ListPolicyDto>? cachedList))
            {
                return cachedList;
            }

            var list = (await _unitOfWork.PolicyRepo.GetAll()).ToList();
            if (list.Count == 0)
            {
                return new List<ListPolicyDto>();
            }

            var mappedList = PolicyMapper.ToListPolicyDto(list);
            _cache.Set(ConstantString.POLICY_CACHE, mappedList, TimeSpan.FromDays(30));

            return mappedList;
        }
    }
}
