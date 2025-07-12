using DannyKeyboard.Application.Common;
using DannyKeyboard.Application.DTOs.User;
using DannyKeyboard.Application.Mappers;
using MediatR;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DannyKeyboard.Application.Features.User.Command
{
    public class LoginHandler : IRequestHandler<LoginCommand, LoginResponseDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _configure;

        public LoginHandler(IUnitOfWork unitOfWork, IConfiguration configure)
        {
            _unitOfWork = unitOfWork;
            _configure = configure;
        }

        public async Task<LoginResponseDto> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();

                var hashedPassword = Sha256Encoding.ComputeSHA256Hash(request.Dto.Password + _configure["SecretString"]);

                var foundUser = await _unitOfWork.UserRepo.GetOneByEmailAndPass(request.Dto.Email, hashedPassword);

                return new LoginResponseDto();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                await _unitOfWork.RollbackTransactionAsync();

                return new LoginResponseDto(); 
            }
        }
    }
}
