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
    public class UserRepository : IUserRepository
    {
        private readonly DannyKeyboardShopDBContext _context;
        public UserRepository(DannyKeyboardShopDBContext context)
        {
            _context = context;
        }

        public async Task<User?> GetCustomerByUserId(string userId)
        {
            return await _context.Users
                .Include(x => x.Role)
                .Include(x => x.Customer)
                .FirstOrDefaultAsync( x => x.UserId == userId 
                                        && x.IsActive);
        }

        public async Task<User?> GetOneByEmail(string email)
        {
            return await _context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Email.Equals(email)
                                  && x.IsActive);
        }

        public async Task<User?> GetOneByEmailAndPass(string email, string password)
        {
            return await _context.Users
                .Include(x => x.Role)
                .FirstOrDefaultAsync(x => x.Email.Equals(email)
                                  && x.Password.Equals(password)
                                  && x.IsActive);
        }

        public async Task<User?> GetOneByRefreshToken(string refreshToken)
        {
            return await _context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.RefreshToken.Equals(refreshToken)
                                  && DateTime.UtcNow <= x.RefreshTokenExpiryTime
                                  && x.IsActive);
        }

        public async Task<User?> GetStaffByUserId(string userId)
        {
            return await _context.Users
               .Include(x => x.Role)
               .Include(x => x.Staff)
               .FirstOrDefaultAsync(x => x.UserId.Trim() == userId
                                       && x.IsActive);
        }

        public async Task InsertUser(User user)
        {
            user.UserId = GenerateId();
            await _context.Users.AddAsync(user);
        }

        public void UpdateUser(User user)
        {
            _context.Users.Update(user);
        }
        private string GenerateId()
        {
            if (!_context.Users.Any())
            {
                return "U0";
            }

            var newNumber = _context.Users
                .Select(u => int.Parse(u.UserId.Substring(1)))
                .ToList().Max() + 1;

            return "U" + newNumber;
        }
    }
}
