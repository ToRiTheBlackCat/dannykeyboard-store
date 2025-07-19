using DannyKeyboard.Application.DTOs.Customer;
using DannyKeyboard.Application.DTOs.Shift;
using DannyKeyboard.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DannyKeyboard.Application.Mappers
{
    public static class ShiftMapper
    {
        public static Shift ToShift(this CreateShiftDto dto)
        {
            return new Shift()
            {
                ShiftName = dto.ShiftName.Trim(),
                StartTime = dto.StartTime,
                EndTime = dto.EndTime,
            };
        }
    }
}
