using DannyKeyboard.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DannyKeyboard.Domain.Interfaces
{
    public interface IStaffRepository
    {
        Task<IEnumerable<Staff>> GetAll();
        Task<Staff?> GetOne(string staffId);

        Task InsertStaff(Staff staff);
        void UpdateStaff(Staff staff);
    }
}
