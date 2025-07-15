using DannyKeyboard.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DannyKeyboard.Application.DTOs.Staff
{
    public class ListStaffResponseDto
    {
        public int StaffCount { get; set; }
        public List<Domain.Entities.Staff> StaffList { get; set; } = new List<Domain.Entities.Staff>();
    }
}
