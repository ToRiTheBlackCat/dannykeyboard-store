using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DannyKeyboard.Application.DTOs.AboutUs
{
    public class UpdateAboutUsDto
    {
        public int AboutUsId { get; set; }

        public string Detail { get; set; } = string.Empty;
    }
}
