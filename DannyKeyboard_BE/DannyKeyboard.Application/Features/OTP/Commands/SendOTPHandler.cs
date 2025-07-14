using DannyKeyboard.Application.Common;
using DannyKeyboard.Application.DTOs.Cache;
using MediatR;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DannyKeyboard.Application.Features.OTP.Commands
{
    public class SendOTPHandler : IRequestHandler<SendOTPCommand, (bool, string)>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _configure;
        private readonly EmailSender emailSender;
        private readonly IMemoryCache _cache;

        private static string SUCCESS = "Send OTP successfully";
        private static string FAIL = "Send OTP fail";

        public SendOTPHandler(IUnitOfWork unitOfWork,
                              IConfiguration configure,
                              IMemoryCache cache)
        {
            _unitOfWork = unitOfWork;
            _configure = configure;
            emailSender = new EmailSender(_configure);
            _cache = cache;
        }

        public async Task<(bool, string)> Handle(SendOTPCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();

                //Create Reset code and send that to email
                var otpCode = emailSender.SendOTP(request.Email);
                if(otpCode == null)
                {
                    return (false,FAIL);
                }

                //Save data to cache wating for otp confirmation
                var tempRegisterCache = new TempSignUpCache
                {
                    OtpCode = otpCode,
                    ExpireAt = DateTime.UtcNow.AddMinutes(15)
                };

                _cache.Set(request.Email, tempRegisterCache, TimeSpan.FromMinutes(15));

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
