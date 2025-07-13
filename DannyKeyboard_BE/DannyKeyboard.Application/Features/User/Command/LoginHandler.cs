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
        private readonly JwtAuthentication _jwtAuth;

        public LoginHandler(IUnitOfWork unitOfWork, 
                            IConfiguration configure,
                            JwtAuthentication jwtAuth)
        {
            _unitOfWork = unitOfWork;
            _configure = configure;
            _jwtAuth = jwtAuth;
        }

        public async Task<LoginResponseDto> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var responseDto = new LoginResponseDto();
            try
            {
                await _unitOfWork.BeginTransactionAsync();

                //Hash the password
                var hashedPassword = Sha256Encoding.ComputeSHA256Hash(request.Dto.Password + _configure["SecretString"]);

                //Find user
                var foundUser = await _unitOfWork.UserRepo.GetOneByEmailAndPass(request.Dto.Email, hashedPassword);

                if (foundUser == null)
                {
                    return responseDto;
                }
                else
                {
                    //Create token for found user
                    var accessToken = _jwtAuth.GenerateAccessToken(foundUser);
                    var refreshToken = _jwtAuth.GenerateRefreshToken();

                    //Update refresh token to foundUser
                    foundUser.RefreshToken = refreshToken;
                    foundUser.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);
                     _unitOfWork.UserRepo.UpdateUser(foundUser);

                    await _unitOfWork.CommitTransactionAsync();

                    //Add data to response
                    responseDto.UserId = foundUser.UserId.Trim();
                    responseDto.RoleId = foundUser.RoleId;
                    responseDto.RoleName = foundUser.Role.RoleName.Trim();
                    responseDto.AccessToken = accessToken;
                    responseDto.RefreshToken = refreshToken;
                    responseDto.RefreshTokenExpiryTime = foundUser.RefreshTokenExpiryTime;

                    return responseDto;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                await _unitOfWork.RollbackTransactionAsync();

                return responseDto; 
            }
        }
    }
}
