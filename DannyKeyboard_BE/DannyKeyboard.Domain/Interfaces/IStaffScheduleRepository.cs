using DannyKeyboard.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DannyKeyboard.Domain.Interfaces
{
    public interface IStaffScheduleRepository
    {
        Task<IEnumerable<StaffSchedule>> GetAll();
        Task<StaffSchedule?> GetOne(int id);
        Task Insert(StaffSchedule staffSchedule);
        void Update(StaffSchedule staffSchedule);
        void Delete(StaffSchedule staffSchedule);
    }
}
