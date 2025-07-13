using DannyKeyboard.Application.Mappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DannyKeyboard.Application.Features.Policy.Commands
{
    public class CreatePolicyHandler : IRequestHandler<CreatePolicyCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        public CreatePolicyHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(CreatePolicyCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();

                var newPolicy = PolicyMapper.ToPolicy(request.Dto);
                await _unitOfWork.PolicyRepo.Insert(newPolicy);
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
