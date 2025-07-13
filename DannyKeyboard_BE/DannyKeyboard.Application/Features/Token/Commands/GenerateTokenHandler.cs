using DannyKeyboard.Application.Common;
using DannyKeyboard.Application.DTOs.Token;
using DannyKeyboard.Application.DTOs.User;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DannyKeyboard.Application.Features.Token.Commands
{
    public class GenerateTokenHandler : IRequestHandler<GenerateTokenCommand, RefreshTokenResponseDto>
    {
        private readonly JwtAuthentication _jwtAuth;
        private readonly IUnitOfWork _unitOfWork;

        public GenerateTokenHandler(IUnitOfWork unitOfWork, JwtAuthentication jwtAuth)
        {
            _unitOfWork = unitOfWork;
            _jwtAuth = jwtAuth;
        }

        public async Task<RefreshTokenResponseDto> Handle(GenerateTokenCommand request, CancellationToken cancellationToken)
        {
            var responseDto = new RefreshTokenResponseDto();
            try
            {
                await _unitOfWork.BeginTransactionAsync();

                //Find exist User
                var foundUser = await _unitOfWork.UserRepo.GetOneByRefreshToken(request.Dto.RefreshToken);
                if (foundUser == null)
                {
                    return responseDto;
                }

                //Create new token
                var newTokens = _jwtAuth.RefreshTokenAsync(foundUser);

                //Update the token
                foundUser.RefreshToken = newTokens.RefreshToken;
                foundUser.RefreshTokenExpiryTime = newTokens.RefreshTokenExpiryTime;

                _unitOfWork.UserRepo.UpdateUser(foundUser);
                await _unitOfWork.CommitTransactionAsync();

                //Add data to response
                responseDto.AccessToken = newTokens.AccessToken;
                responseDto.RefreshToken = newTokens.RefreshToken;
                responseDto.RefreshTokenExpiryTime = newTokens.RefreshTokenExpiryTime;

                return responseDto;

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
