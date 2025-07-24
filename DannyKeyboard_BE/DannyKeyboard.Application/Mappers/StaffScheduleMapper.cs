using DannyKeyboard.Application.DTOs.Customer;
using DannyKeyboard.Application.DTOs.StaffSchedule;
using DannyKeyboard.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DannyKeyboard.Application.Mappers
{
    public static class StaffScheduleMapper
    {
        public static StaffSchedule ToStaffSchedule(this CreateScheduleOfStaffDto dto)
        {
            return new StaffSchedule()
            {
                ShiftId = dto.ShiftId,
                StaffId = dto.StaffId,
                Tasks = dto.Tasks,
                WorkDate = dto.WorkDate,
                Note = dto.Note,
                IsPresent = dto.IsPresent
            };
        }
    }
}
