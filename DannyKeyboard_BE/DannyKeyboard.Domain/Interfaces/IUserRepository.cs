using DannyKeyboard.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DannyKeyboard.Domain.Interfaces
{
    public interface IUserRepository
    {
        Task<User?> GetOneByEmailAndPass(string email, string password);
        void UpdateUser(User user);
        Task<User?> GetOneByRefreshToken(string refreshToken);

    }
}
