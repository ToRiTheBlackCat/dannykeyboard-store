using DannyKeyboard.Application.Common;
using DannyKeyboard.Application.DTOs.AboutUs;
using DannyKeyboard.Application.Mappers;
using DannyKeyboard.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DannyKeyboard.Application.Features.AboutUs.Queries
{
    public record GetAllAboutUsQuery : IRequest<List<ListAboutUsDto>>;

    public class GetAllAboutUsHandler : IRequestHandler<GetAllAboutUsQuery, List<ListAboutUsDto>?>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMemoryCache _cache;
        public GetAllAboutUsHandler(IUnitOfWork unitOfWork, IMemoryCache cache)
        {
            _unitOfWork = unitOfWork;
            _cache = cache;
        }

        public async Task<List<ListAboutUsDto>?> Handle(GetAllAboutUsQuery request, CancellationToken cancellationToken)
        {
            if (_cache.TryGetValue(ConstantString.ABOUTUS_CACHE, out List<ListAboutUsDto>? cachedList))
            {
                return cachedList;
            }


            var list = (await _unitOfWork.AboutUsRepo.GetAll()).ToList();
            if (list.Count == 0)
            {
                return new List<ListAboutUsDto>();
            }

            var mappedList = AboutUsMapper.ToListAboutUsDto(list);
            _cache.Set(ConstantString.ABOUTUS_CACHE, mappedList, TimeSpan.FromDays(30));

            return mappedList;
        }
    }
}
