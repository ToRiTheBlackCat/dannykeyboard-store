using DannyKeyboard.Application.DTOs.Policy;
using DannyKeyboard.Application.DTOs.Role;
using DannyKeyboard.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DannyKeyboard.Application.Mappers
{
    public static class RoleMapper
    {
        public static Role ToRole(this CreateRoleDto dto)
        {
            return new Role()
            {
                RoleName = dto.RoleName,
            };
        }

        public static Role ToRole(this UpdateRoleDto dto)
        {
            return new Role()
            {
                RoleId = dto.RoleId,
                RoleName = dto.RoleName,
            };
        }
    }
}
