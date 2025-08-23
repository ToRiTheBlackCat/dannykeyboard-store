using DannyKeyboard.Application.Common;
using DannyKeyboard.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DannyKeyboard.Application.Features.Brand.Queries
{
    public class GetAllBrandQuery : IRequest<List<Domain.Entities.Brand>>;

    public class GetAllBrandHandler : IRequestHandler<GetAllBrandQuery, List<Domain.Entities.Brand>?>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMemoryCache _cache;

        public GetAllBrandHandler(IUnitOfWork unitOfWork, IMemoryCache cache)
        {
            _unitOfWork = unitOfWork;
            _cache = cache;
        }

        public async Task<List<Domain.Entities.Brand>?> Handle(GetAllBrandQuery request, CancellationToken cancellationToken)
        {
            if (_cache.TryGetValue(ConstantString.BRAND_CACHE, out List<Domain.Entities.Brand>? cachedList))
            {
                return cachedList;
            }

            var list = (await _unitOfWork.BrandRepo.GetAll()).ToList();
            _cache.Set(ConstantString.BRAND_CACHE, list, TimeSpan.FromDays(1));

            return list;
        }
    }
}
