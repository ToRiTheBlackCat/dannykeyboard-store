using DannyKeyboard.Application.DTOs.StaffSchedule;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DannyKeyboard.Application.Features.StaffSchedule.Commands
{
    public class UpdateScheduleForStaffCommand : IRequest<(bool,string)>
    {
        public int ScheduleId { get; set; }
        public UpdateScheduleOfStaffDto Dto { get; set; }
        public UpdateScheduleForStaffCommand(int scheduleId, UpdateScheduleOfStaffDto dto)
        {
            ScheduleId = scheduleId;
            Dto = dto;
        }
    }
}
