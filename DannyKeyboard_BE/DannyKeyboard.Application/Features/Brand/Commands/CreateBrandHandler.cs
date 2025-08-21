using DannyKeyboard.Application.Mappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DannyKeyboard.Application.Features.Brand.Commands
{
    public class CreateBrandHandler : IRequestHandler<CreateBrandCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        public CreateBrandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(CreateBrandCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();

                var newBrand = BrandMapper.ToBrand(request.Dto);
                if (newBrand == null)
                {
                    return false;
                }

                await _unitOfWork.BrandRepo.Insert(newBrand);
                await _unitOfWork.CommitTransactionAsync();

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                await _unitOfWork.RollbackTransactionAsync();

                return false;
            }
        }
    }
}
