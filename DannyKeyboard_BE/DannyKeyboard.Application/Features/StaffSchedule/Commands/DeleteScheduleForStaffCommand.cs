using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DannyKeyboard.Application.Features.StaffSchedule.Commands
{
    public class DeleteScheduleForStaffCommand : IRequest<(bool,string)>
    {
        public int SheduleId { get; set; }
        public DeleteScheduleForStaffCommand(int scheduleId)
        {
            SheduleId = scheduleId;
        }
    }
}
