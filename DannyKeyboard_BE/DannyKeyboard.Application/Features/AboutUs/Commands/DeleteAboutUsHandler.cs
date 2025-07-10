using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DannyKeyboard.Application.Features.AboutUs.Commands
{
    public class DeleteAboutUsHandler : IRequestHandler<DeleteAboutUsCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteAboutUsHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(DeleteAboutUsCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();

                var entity = await _unitOfWork.AboutUsRepo.GetOne(request.Id);
                if (entity == null)
                {
                    await _unitOfWork.RollbackTransactionAsync();
                    return false;
                }

                _unitOfWork.AboutUsRepo.Delete(entity);
                await _unitOfWork.CommitTransactionAsync();

                return true;
            }
            catch
            {
                await _unitOfWork.RollbackTransactionAsync();
                return false;
            }
        }
    }
}
