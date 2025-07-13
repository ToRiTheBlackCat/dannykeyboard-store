using DannyKeyboard.Application.Features.AboutUs.Commands;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DannyKeyboard.Application.Features.Policy.Commands
{
    public class DeletePolicyHandler : IRequestHandler<DeletePolicyCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeletePolicyHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(DeletePolicyCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();

                var policy = await _unitOfWork.PolicyRepo.GetOne(request.Id);
                if (policy == null)
                {
                    await _unitOfWork.RollbackTransactionAsync();
                    return false;
                }

                _unitOfWork.PolicyRepo.Delete(policy);
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
