using DannyKeyboard.Application.Common;
using DannyKeyboard.Application.DTOs.AboutUs;
using DannyKeyboard.Application.Features.AboutUs.Queries;
using MediatR;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DannyKeyboard.Application.Features.Role.Queries
{
    public record GetAllRoleQuery : IRequest<List<Domain.Entities.Role>>;
    public class GetAllRoleHandler : IRequestHandler<GetAllRoleQuery, List<Domain.Entities.Role>?>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMemoryCache _cache;


        public GetAllRoleHandler(IUnitOfWork unitOfWork, IMemoryCache cache)
        {
            _unitOfWork = unitOfWork;
            _cache = cache;
        }

        public async Task<List<Domain.Entities.Role>?> Handle(GetAllRoleQuery request, CancellationToken cancellationToken)
        {
            if (_cache.TryGetValue(ConstantString.ROLE_CACHE, out List<Domain.Entities.Role>? cachedList))
            {
                return cachedList;
            }

            var list = (await _unitOfWork.RoleRepo.GetAll()).ToList();
            _cache.Set(ConstantString.ROLE_CACHE, list, TimeSpan.FromDays(30));

            return list;
        }
    }
}
