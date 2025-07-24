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
    public class StaffRepository : IStaffRepository
    {
        private readonly DannyKeyboardShopDBContext _context;
        public StaffRepository(DannyKeyboardShopDBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Staff>> GetAll()
        {
            return await _context.Staff
                .ToListAsync();
        }

        public async Task<Staff?> GetOne(string staffId)
        {
            return await _context.Staff
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.StaffId.Equals(staffId));
        }

        public async Task InsertStaff(Staff staff)
        {
            await _context.Staff.AddAsync(staff);
        }

        public void UpdateStaff(Staff staff)
        {
            _context.Staff.Update(staff);
        }
    }
}
