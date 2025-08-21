using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DannyKeyboard.Application.Features.Brand.Commands
{
    public class DeleteBrandHandler : IRequestHandler<DeleteBrandCommand, (bool, string)>
    {
        private readonly IUnitOfWork _unitOfWork;
        private static string SUCCESS = "Delete brand successfully";
        private static string ERROR = "Error when delete brand";
        private static string NOTFOUND = "Delete fail. Not found any brand with that id";
        public DeleteBrandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<(bool, string)> Handle(DeleteBrandCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();

                //Find existed brand
                var foundBrand = await _unitOfWork.BrandRepo.GetOne(request.BrandId);
                if (foundBrand == null)
                {
                    return (false, NOTFOUND);
                }

                _unitOfWork.BrandRepo.Delete(foundBrand);
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
