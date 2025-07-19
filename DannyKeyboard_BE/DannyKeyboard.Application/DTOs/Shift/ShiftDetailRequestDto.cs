using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DannyKeyboard.Application.DTOs.Shift
{
    public class ShiftDetailRequestDto
    {
        [Required]
        public int ShiftId { get; set; }
    }
}
