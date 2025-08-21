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
    public class BrandRepository : IBrandRepository
    {
        private readonly DannyKeyboardShopDBContext _context;
        public BrandRepository(DannyKeyboardShopDBContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Brand>> GetAll()
        {
            return await _context.Brands
                .Where(x => x.IsActive)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Brand?> GetOne(int id)
        {
            return await _context.Brands
                .FirstOrDefaultAsync(x => x.BrandId == id
                && x.IsActive);
        }

        public async Task Insert(Brand brand)
        {
            await _context.Brands.AddAsync(brand);
        }

        public void Update(Brand brand)
        {
            _context.Brands.Update(brand);
        }

        public void Delete(Brand brand)
        {
            _context.Brands.Remove(brand);
        }
    }
}
