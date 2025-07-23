using DannyKeyboard.Domain.Entities;
using DannyKeyboard.Domain.Interfaces;
using DannyKeyboard.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DannyKeyboard.Infrastructure.Repositories
{
    public class StaffScheduleRepository : IStaffScheduleRepository
    {
        private readonly DannyKeyboardShopDBContext _context;
        public StaffScheduleRepository(DannyKeyboardShopDBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<StaffSchedule>> GetAll()
        {
            return await _context.StaffSchedules.ToListAsync();
        }

        public async Task<StaffSchedule?> GetOne(int id)
        {
            return await _context.StaffSchedules
                .Include(x => x.Staff)
                .Include(x => x.Shift)
                .FirstOrDefaultAsync(x => x.ScheduleId == id);
        }

        public async Task Insert(StaffSchedule staffSchedule)
        {
            await _context.StaffSchedules.AddAsync(staffSchedule);
        }

        public void Update(StaffSchedule staffSchedule)
        {
            _context.StaffSchedules.Update(staffSchedule);
        }
        public void Delete(StaffSchedule staffSchedule)
        {
            _context.StaffSchedules.Remove(staffSchedule);
        }
    }
}
