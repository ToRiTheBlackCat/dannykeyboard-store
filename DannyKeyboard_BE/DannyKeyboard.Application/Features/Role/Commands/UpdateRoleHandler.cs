using DannyKeyboard.Application.Mappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DannyKeyboard.Application.Features.Role.Commands
{
    public class UpdateRoleHandler : IRequestHandler<UpdateRoleCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        public UpdateRoleHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<bool> Handle(UpdateRoleCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();

                var updateRole = RoleMapper.ToRole(request.Dto);

                _unitOfWork.RoleRepo.Update(updateRole);
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
