using DannyKeyboard.Application.DTOs.AboutUs;
using DannyKeyboard.Application.DTOs.Staff;
using DannyKeyboard.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DannyKeyboard.Application.Mappers
{
    public static class StaffMapper
    {
        public static StaffProfileResponseDto ToStaffProfileResponseDto(this User user)
        {
            return new StaffProfileResponseDto()
            {
                StaffId = user.Staff.StaffId.Trim(),
                Email = user.Email,
                RoleId = user.RoleId,
                RoleName = user.Role.RoleName,
                FullName = user.Staff.FullName,
                Address = user.Staff.Address,
                Phone = user.Staff.Phone,
                Dob = user.Staff.Dob
            };
        }
    }
}
