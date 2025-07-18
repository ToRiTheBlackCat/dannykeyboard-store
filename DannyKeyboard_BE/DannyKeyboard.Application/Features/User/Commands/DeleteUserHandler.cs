using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DannyKeyboard.Application.Features.User.Commands
{
    public class DeleteUserHandler : IRequestHandler<DeleteUserCommand, (bool, string)>
    {
        private readonly IUnitOfWork _unitOfWork;
        private static string NOTFOUNDUSER = "Cannot find User with that userId";
        private static string SUCCESS = "Delete user successfully";
        private static string FAIL = "Delete user fail";

        public DeleteUserHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<(bool, string)> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var foundUser = new Domain.Entities.User();
            try
            {
                await _unitOfWork.BeginTransactionAsync();

                //Check role
                if (request.Dto.IsStaff)
                {
                    foundUser = await _unitOfWork.UserRepo.GetStaffByUserId(request.Dto.UserId);
                }
                else
                {
                    foundUser = await _unitOfWork.UserRepo.GetCustomerByUserId(request.Dto.UserId);
                }

                if (foundUser == null)
                {
                    return (false, NOTFOUNDUSER);
                }

                //Unactive user
                foundUser.IsActive = false;
                _unitOfWork.UserRepo.UpdateUser(foundUser);
                await _unitOfWork.CommitTransactionAsync();

                return (true, SUCCESS);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                await _unitOfWork.RollbackTransactionAsync();
                return (false, FAIL);
            }
        }
    }
}
