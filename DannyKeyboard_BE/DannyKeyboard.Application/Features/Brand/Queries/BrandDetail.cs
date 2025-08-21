using MediatR;
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
        public BrandDetailHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Domain.Entities.Brand?> Handle(BrandDetailQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var response = await _unitOfWork.BrandRepo.GetOne(request.id);

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
