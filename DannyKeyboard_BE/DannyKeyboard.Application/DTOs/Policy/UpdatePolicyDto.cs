using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DannyKeyboard.Application.DTOs.Policy
{
    public class UpdatePolicyDto
    {
        [Required]
        public int PolicyId { get; set; }
        [Required]
        public string PolicyName { get; set; } = string.Empty;
    }
}
