using DannyKeyboard.Application.Features.Policy.Commands;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DannyKeyboard.Application.Features.Role.Commands
{
    public class DeleteRoleHandler : IRequestHandler<DeleteRoleCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteRoleHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();

                var role = await _unitOfWork.RoleRepo.GetOne(request.Id);
                if (role == null)
                {
                    await _unitOfWork.RollbackTransactionAsync();
                    return false;
                }

                _unitOfWork.RoleRepo.Delete(role);
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
