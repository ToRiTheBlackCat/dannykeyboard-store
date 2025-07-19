using DannyKeyboard.Application.Common;
using DannyKeyboard.Application.DTOs.Cache;
using DannyKeyboard.Application.DTOs.User;
using DannyKeyboard.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DannyKeyboard.Application.Features.Customer.Commands
{
    public class CustomerSignUpHandler : IRequestHandler<CustomerSignUpCommand, (bool, string)>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMemoryCache _cache;
        private readonly IConfiguration _configure;

        private static string SUCCESS = "Create user successfully";
        private static string FAIL = "Create user fail";
        private static string ERROR = "Error when create new user";
        private static string EXIST = "Already exist user with that emial. Try other email to sign up";


        public CustomerSignUpHandler(IUnitOfWork unitOfWork,
                                    IConfiguration configure,
                                     IMemoryCache cache)
        {
            _unitOfWork = unitOfWork;
            _configure = configure;
            _cache = cache;
        }

        public async Task<(bool, string)> Handle(CustomerSignUpCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();

                //Check OTP code in cache
                //Valid - Register new user - customer
                if (ValidateOtp(request.Dto.Email, request.Dto.OtpCode))
                {
                    //Check existed User with that email
                    var foundUser = await _unitOfWork.UserRepo.GetOneByEmail(request.Dto.Email);
                    if (foundUser != null)
                    {
                        return (false, EXIST);
                    }

                    //Hash the password
                    var hashedPassword = Sha256Encoding.ComputeSHA256Hash(request.Dto.Password + _configure["SecretString"]);

                    //Create new User
                    var newUser = new Domain.Entities.User
                    {
                        Email = request.Dto.Email,
                        Password = hashedPassword,
                        CreatedDate = DateOnly.FromDateTime(DateTime.Now),
                        RoleId = 3,
                        IsActive = true
                    };
                    await _unitOfWork.UserRepo.InsertUser(newUser);
                    await _unitOfWork.SaveChangesAsync();

                    //Create new Customer
                    var newCustomer = new Domain.Entities.Customer
                    {
                        CustomerId = newUser.UserId,
                        FullName = request.Dto.FullName,
                        Address = request.Dto.Address,
                        Phone = request.Dto.Phone,
                        Dob = request.Dto.Dob,
                    };
                    await _unitOfWork.CustomerRepo.InsertCustomer(newCustomer);
                    await _unitOfWork.SaveChangesAsync();
                    await _unitOfWork.CommitTransactionAsync();

                    //Remove existed Otp in cache
                    RemoveOtp(request.Dto.Email);

                    return (true, SUCCESS);
                }

                return (false, FAIL);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                await _unitOfWork.RollbackTransactionAsync();
                return (false, ERROR);
            }
        }

        private bool ValidateOtp(string email, string inputOtp)
        {
            if (_cache.TryGetValue(email, out TempSignUpCache? cacheOtp))
            {
                return cacheOtp?.ExpireAt >= DateTime.UtcNow && cacheOtp?.OtpCode == inputOtp;
            }

            return false;
        }

        private void RemoveOtp(string email)
        {
            _cache.Remove(email);
        }
    }
}
