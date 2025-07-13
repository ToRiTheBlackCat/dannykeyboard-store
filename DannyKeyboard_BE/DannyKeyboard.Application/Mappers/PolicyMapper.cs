using DannyKeyboard.Application.DTOs.AboutUs;
using DannyKeyboard.Application.DTOs.Policy;
using DannyKeyboard.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DannyKeyboard.Application.Mappers
{
    public static class PolicyMapper
    {
        public static List<ListPolicyDto> ToListPolicyDto(this List<Policy> list)
        {
            var listDto = new List<ListPolicyDto>();

            foreach (var item in list)
            {
                var dto = new ListPolicyDto
                {
                    PolicyId = item.PolicyId,
                    PolicyName = item.PolicyName,   
                };
                listDto.Add(dto);
            }
            return listDto;
        }
        public static Policy ToPolicy(this CreatePolicyDto dto)
        {
            return new Policy()
            {
                PolicyName = dto.PolicyName,
                IsActive = true
            };
        }
        public static Policy ToPolicy(this UpdatePolicyDto dto)
        {
            return new Policy()
            {
                PolicyId = dto.PolicyId,
                PolicyName = dto.PolicyName,
                IsActive = true
            };
        }
    }
}
