using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DannyKeyboard.Application.DTOs.User
{
    public class DeleteUserRequestDto
    {
        [Required]
        public string UserId { get; set; } = string.Empty;
        [Required]
        public bool IsStaff { get; set; }
    }
}
