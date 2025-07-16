using DannyKeyboard.Application.DTOs.AboutUs;
using DannyKeyboard.Application.DTOs.Customer;
using DannyKeyboard.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DannyKeyboard.Application.Mappers
{
    public static class CustomerMapper
    {
        public static UpdateCustomerProfileDto ToUpdateCustomerProfileDto(this User user)
        {
            return new UpdateCustomerProfileDto()
            {
                CustomerId = user.Customer.CustomerId.Trim(),
                FullName = user.Customer.FullName,
                Address = user.Customer.Address,
                Phone = user.Customer.Phone,
                Dob = user.Customer.Dob
            };
        }
    }
}
