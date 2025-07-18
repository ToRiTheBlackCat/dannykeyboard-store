using DannyKeyboard.Application.Common;
using MediatR;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DannyKeyboard.Application.Features.Password.Commands
{
    public class ResetPasswordHandler : IRequestHandler<ResetPasswordCommand, (bool, string)>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _configure;
        private static string NOTFOUNDUSER = "Cannot find User with that email";
        private static string NOTCORRECTCODE = "Reset code is not correct";
        private static string SUCCESS = "Reset password successfully";
        private static string FAIL = "Reset password fail";

        public ResetPasswordHandler(IUnitOfWork unitOfWork,                     
                                    IConfiguration configure)
        {
            _unitOfWork = unitOfWork;
            _configure = configure;
        }

        public async Task<(bool, string)> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();

                //Find existed User
                var foundUser = await _unitOfWork.UserRepo.GetOneByEmail(request.Dto.Email);
                if (foundUser == null)
                {
                    return (false, NOTFOUNDUSER);
                }

                //Hash ResetCode to compare
                var hashedResetCode = Sha256Encoding.ComputeSHA256Hash(request.Dto.ResetCode + _configure["SecretString"]);
                if (foundUser.ResetCode.Trim() != hashedResetCode)
                {
                    return (false, NOTCORRECTCODE);
                }

                //Hash the password
                var hashedPassword = Sha256Encoding.ComputeSHA256Hash(request.Dto.NewPassword + _configure["SecretString"]);

                //Save new password to user
                foundUser.Password = hashedPassword;
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
