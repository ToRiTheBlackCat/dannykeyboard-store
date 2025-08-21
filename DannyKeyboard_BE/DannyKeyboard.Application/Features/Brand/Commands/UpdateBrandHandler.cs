using DannyKeyboard.Application.Mappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DannyKeyboard.Application.Features.Brand.Commands
{
    public class UpdateBrandHandler : IRequestHandler<UpdateBrandCommand, (bool, string)>
    {
        private readonly IUnitOfWork _unitOfWork;

        private static string SUCCESS = "Update brand successfully";
        private static string ERROR = "Error when update brand";
        private static string NOTFOUND = "Update fail. Not found any brand with that id";

        public UpdateBrandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<(bool, string)> Handle(UpdateBrandCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();

                //Find existed brand
                var foundBrand = await _unitOfWork.BrandRepo.GetOne(request.Dto.BrandId);
                if (foundBrand == null)
                {
                    return (false, NOTFOUND);
                }

                var updateBrand = BrandMapper.ToBrand(request.Dto, foundBrand);

                _unitOfWork.BrandRepo.Update(updateBrand);
                await _unitOfWork.CommitTransactionAsync();

                return (true, SUCCESS);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                await _unitOfWork.RollbackTransactionAsync();

                return (false, ERROR);
            }
        }
    }
}
