using DannyKeyboard.Application.Mappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DannyKeyboard.Application.Features.Policy.Commands
{
    public class UpdatePolicyHandler : IRequestHandler<UpdatePolicyCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        public UpdatePolicyHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(UpdatePolicyCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();

                var updatePolicy = PolicyMapper.ToPolicy(request.Dto);
                if (updatePolicy == null)
                {
                    return false;
                }

                _unitOfWork.PolicyRepo.Update(updatePolicy);
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
