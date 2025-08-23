using DannyKeyboard.Application.Common;
using MediatR;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DannyKeyboard.Application.Features.Brand.Queries
{
    public record BrandDetailQuery(int id) : IRequest<Domain.Entities.Brand?>;

    public class BrandDetailHandler : IRequestHandler<BrandDetailQuery, Domain.Entities.Brand?>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMemoryCache _cache;

        public BrandDetailHandler(IUnitOfWork unitOfWork, IMemoryCache cache)
        {
            _unitOfWork = unitOfWork;
            _cache = cache;
        }

        public async Task<Domain.Entities.Brand?> Handle(BrandDetailQuery request, CancellationToken cancellationToken)
        {
            try
            {
                if (_cache.TryGetValue(ConstantString.BRANDDETAIL_CACHE + request.id, out Domain.Entities.Brand? cachedBrandDetail))
                {
                    return cachedBrandDetail;
                }

                var response = await _unitOfWork.BrandRepo.GetOne(request.id);
                _cache.Set(ConstantString.BRANDDETAIL_CACHE + request.id, response, TimeSpan.FromDays(1));

                return response;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return new Domain.Entities.Brand();
            }
        }
    }
}
