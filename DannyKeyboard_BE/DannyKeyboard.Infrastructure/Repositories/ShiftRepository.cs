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
    public class ShiftRepository : IShiftRepository
    {
        private readonly DannyKeyboardShopDBContext _context;
        public ShiftRepository(DannyKeyboardShopDBContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Shift>> GetAll()
        {
            return await _context.Shifts
                .AsNoTracking()
                .ToListAsync();
        }
        public async Task<Shift?> GetOne(int id)
        {
            return await _context.Shifts
                .FirstOrDefaultAsync(x => x.ShiftId == id);
        }
        public async Task Insert(Shift shift)
        {
            await _context.Shifts.AddAsync(shift);
        }
        public void Update(Shift shift)
        {
            _context.Shifts.Update(shift);
        }
        public void Delete(Shift shift)
        {
            _context.Shifts.Remove(shift);
        }
    }
}
