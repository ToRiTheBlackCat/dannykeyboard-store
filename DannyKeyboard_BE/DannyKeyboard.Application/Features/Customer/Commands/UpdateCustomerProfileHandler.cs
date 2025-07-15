using DannyKeyboard.Application.DTOs.Customer;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DannyKeyboard.Application.Features.Customer.Commands
{
    public class UpdateCustomerProfileHandler : IRequestHandler<UpdateCustomerProfileCommand, UpdateCustomerProfileDto>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateCustomerProfileHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<UpdateCustomerProfileDto> Handle(UpdateCustomerProfileCommand request, CancellationToken cancellationToken)
        {

            try
            {
                await _unitOfWork.BeginTransactionAsync();

                //Find existed user
                var foundCustomer = await _unitOfWork.UserRepo.GetCustomerByUserId(request.Dto.CustomerId);
                if (foundCustomer == null)
                {
                    return new();
                }

                //Update data to found Customer
                foundCustomer.Customer.FullName = request.Dto.FullName;
                foundCustomer.Customer.Address = request.Dto.Address;
                foundCustomer.Customer.Phone = request.Dto.Phone;
                foundCustomer.Customer.Dob = request.Dto.Dob;

                _unitOfWork.CustomerRepo.UpdateCustomer(foundCustomer.Customer);
                await _unitOfWork.CommitTransactionAsync();

                var updatedCustomer = await _unitOfWork.UserRepo.GetCustomerByUserId(foundCustomer.UserId);
                if (updatedCustomer == null)
                {
                    return new();
                }

                var response = new UpdateCustomerProfileDto
                {
                    CustomerId = updatedCustomer.Customer.CustomerId.Trim(),
                    FullName = updatedCustomer.Customer.FullName,
                    Address = updatedCustomer.Customer.Address,
                    Phone = updatedCustomer.Customer.Phone,
                    Dob = updatedCustomer.Customer.Dob
                };

                return response;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                await _unitOfWork.RollbackTransactionAsync();
                return new();
            }
        }
    }
}
