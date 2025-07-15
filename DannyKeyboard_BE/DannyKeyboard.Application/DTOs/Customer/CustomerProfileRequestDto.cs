using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DannyKeyboard.Application.DTOs.Customer
{
    public class CustomerProfileRequestDto
    {
        [Required]
        public string CustomerId { get; set; } = string.Empty;
    }
}
