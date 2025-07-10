using DannyKeyboard.Application.DTOs.AboutUs;
using DannyKeyboard.Application.Mappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DannyKeyboard.Application.Features.AboutUs.Queries
{
    public record GetAllAboutUsQuery : IRequest<List<ListAboutUsDto>>;

    public class GetAllAboutUsHandler : IRequestHandler<GetAllAboutUsQuery, List<ListAboutUsDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllAboutUsHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<ListAboutUsDto>> Handle(GetAllAboutUsQuery request, CancellationToken cancellationToken)
        {
            var list = (await _unitOfWork.AboutUsRepo.GetAll()).ToList();

            return AboutUsMapper.ToListAboutUsDto(list);
        }
    }
}
