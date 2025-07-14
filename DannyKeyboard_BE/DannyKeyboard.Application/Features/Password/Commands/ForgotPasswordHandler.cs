using DannyKeyboard.Application.Common;
using DannyKeyboard.Application.DTOs.Password;
using MediatR;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DannyKeyboard.Application.Features.Password.Commands
{
    public class ForgotPasswordHandler : IRequestHandler<ForgotPasswordCommand, ResponseForgotPasswordDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly EmailSender emailSender;
        private readonly IConfiguration _configure;
        private static string SUCCESS = "Send to email success";
        private static string FAIL = "Send to email fail";


        public ForgotPasswordHandler(IUnitOfWork unitOfWork, IConfiguration configure)
        {
            _unitOfWork = unitOfWork;
            _configure = configure;
            emailSender = new EmailSender(_configure);
        }
        public async Task<ResponseForgotPasswordDto> Handle(ForgotPasswordCommand request, CancellationToken cancellationToken)
        {
            var response = new ResponseForgotPasswordDto();
            try
            {
                await _unitOfWork.BeginTransactionAsync();

                //Find existed user
                var foundUser = await _unitOfWork.UserRepo.GetOneByEmail(request.Email);
                if (foundUser == null)
                {
                    return response;
                }

                //Create Reset code and send that to email
                var resetCode = emailSender.SendPasswordReset(request.Email);

                //Hash the Reset code and save to DB for that user
                foundUser.ResetCode = Sha256Encoding.ComputeSHA256Hash(resetCode + _configure["SecretString"]);
                 _unitOfWork.UserRepo.UpdateUser(foundUser);

                await _unitOfWork.CommitTransactionAsync();

                //Response status
                response.IsSend = true;
                response.Message = SUCCESS;
                return response;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                await _unitOfWork.RollbackTransactionAsync();

                response.IsSend = false;
                response.Message = FAIL;
                return response;
            }
        }
    }
}
