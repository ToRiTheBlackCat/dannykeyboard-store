using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DannyKeyboard.Application.DTOs.Shift
{
    public class UpdateShiftDto
    {
        [Required]
        public int ShiftId { get; set; }
        [Required]
        public string ShiftName { get; set; } = string.Empty;
        [Required]
        public TimeOnly StartTime { get; set; }

        public TimeOnly EndTime { get; set; }


    }
}
