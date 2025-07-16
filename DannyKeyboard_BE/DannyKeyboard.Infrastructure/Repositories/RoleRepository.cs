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
    public class RoleRepository : IRoleRepository
    {
        private readonly DannyKeyboardShopDBContext _context;
        public RoleRepository(DannyKeyboardShopDBContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Role>> GetAll()
        {
            return await _context.Roles
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Role?> GetOne(int id)
        {
            return await _context.Roles
                .FirstOrDefaultAsync(x => x.RoleId == id);
        }

        public async Task Insert(Role role)
        {
            await _context.Roles.AddAsync(role);
        }

        public void Update(Role role)
        {
            _context.Roles.Update(role);
        }

        public void Delete(Role role)
        {
            _context.Roles.Remove(role);
        }
    }
}
