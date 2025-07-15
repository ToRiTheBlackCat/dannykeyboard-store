using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DannyKeyboard.Application.DTOs.Staff
{
    public class StaffProfileRequestDto
    {
        [Required]
        public string StaffId { get; set; } = string.Empty;
    }
}
