using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DannyKeyboard.Application.DTOs.StaffSchedule
{
    public class CreateScheduleOfStaffDto
    {
        [Required]
        public string StaffId { get; set; } = string.Empty;

        public int ShiftId { get; set; }
        [Required]
        public string Tasks { get; set; } = string.Empty;

        public DateOnly WorkDate { get; set; }

        public bool IsPresent { get; set; }

        public string Note { get; set; } = string.Empty;
    }
}
