using DannyKeyboard.Domain.Entities;
using DannyKeyboard.Domain.Interfaces;
using DannyKeyboard.Infrastructure.Base;
using DannyKeyboard.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DannyKeyboard.Infrastructure.Repositories
{
    public class AboutUsRepository : IAboutUsRepository
    {
        private readonly DannyKeyboardShopDBContext _context;
        public AboutUsRepository(DannyKeyboardShopDBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<AboutUs>> GetAll()
        {
            return await _context.AboutUs
                .Where(x => x.IsActive)
                .AsNoTracking()
                .ToListAsync();
        }
        public async Task<AboutUs?> GetOne(int id)
        {
            return await _context.AboutUs
                .FirstOrDefaultAsync(x => x.AboutUsId == id
                && x.IsActive);
        }

        public async Task Insert(AboutUs aboutUs)
        {
            await _context.AboutUs.AddAsync(aboutUs);
        }

        public void Update(AboutUs aboutUs)
        {
            _context.AboutUs.Update(aboutUs);
        }
        public void Delete(AboutUs aboutUs)
        {
            _context.AboutUs.Remove(aboutUs);
        }
    }
}
