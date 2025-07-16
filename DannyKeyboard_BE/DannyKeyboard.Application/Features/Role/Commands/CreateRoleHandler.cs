using DannyKeyboard.Application.Mappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DannyKeyboard.Application.Features.Role.Commands
{
    public class CreateRoleHandler : IRequestHandler<CreateRoleCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        public CreateRoleHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<bool> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();

                var newRole = RoleMapper.ToRole(request.Dto);
                await _unitOfWork.RoleRepo.Insert(newRole);
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
