using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DannyKeyboard.Application.DTOs.Brand
{
    public class CreateBrandDto
    {
        [Required]
        public string BrandName { get; set; } = string.Empty;
        [Required]
        public bool IsActive { get; set; } 
    }
}
