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
    public class PolicyRepository : IPolicyRepository
    {
        private readonly DannyKeyboardShopDBContext _context;
        public PolicyRepository(DannyKeyboardShopDBContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Policy>> GetAll()
        {
            return await _context.Policies
                .Where(x => x.IsActive)
                .AsNoTracking()
                .ToListAsync();
        }
        public async Task<Policy?> GetOne(int id)
        {
            return await _context.Policies
                .FirstOrDefaultAsync(x => x.PolicyId == id
                && x.IsActive);
        }
        public async Task Insert(Policy policy)
        {
            await _context.Policies.AddAsync(policy);
        }
        public void Update(Policy policy)
        {
            _context.Policies.Update(policy);
        }
        public void Delete(Policy policy)
        {
            _context.Policies.Remove(policy);
        }
    }
}
