using DannyKeyboard.Application.Common;
using MediatR;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DannyKeyboard.Application.Features.User.Commands
{
    public class FindExistEmailUserHandler : IRequestHandler<FindExistEmailUserCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        public FindExistEmailUserHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(FindExistEmailUserCommand request, CancellationToken cancellationToken)
        {
            bool isFound = true;
            try
            {
                await _unitOfWork.BeginTransactionAsync();

                //Find existed User
                var foundUser = await _unitOfWork.UserRepo.GetOneByEmail(request.Email);
                if (foundUser == null)
                {
                    return !isFound;
                }

                return isFound;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                await _unitOfWork.RollbackTransactionAsync();
                return !isFound;
            }
        }
    }
}
