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

        public void UpdateUser(User user)
        {
            _context.Users.Update(user);
        }
    }
}
