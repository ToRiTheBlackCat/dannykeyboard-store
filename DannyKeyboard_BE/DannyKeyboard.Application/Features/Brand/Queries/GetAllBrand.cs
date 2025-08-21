using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DannyKeyboard.Application.Features.Brand.Queries
{
    public class GetAllBrandQuery : IRequest<List<Domain.Entities.Brand>>;

    public class GetAllBrandHandler : IRequestHandler<GetAllBrandQuery, List<Domain.Entities.Brand>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetAllBrandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<Domain.Entities.Brand>> Handle(GetAllBrandQuery request, CancellationToken cancellationToken)
        {
            return (await _unitOfWork.BrandRepo.GetAll()).ToList();
        }
    }
}
