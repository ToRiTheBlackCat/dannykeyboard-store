using DannyKeyboard.Application.DTOs.StaffSchedule;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DannyKeyboard.Application.Features.StaffSchedule.Commands
{
    public class CreateScheduleForStaffCommand : IRequest<(bool, string)>
    {
        public string StaffId { get; set; }
        public CreateScheduleOfStaffDto Dto { get; set; }
        public CreateScheduleForStaffCommand(string staffId, CreateScheduleOfStaffDto dto)
        {
            StaffId = staffId;
            Dto = dto;
        }
    }
}
