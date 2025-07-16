using DannyKeyboard.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DannyKeyboard.Domain.Interfaces
{
    public interface IRoleRepository
    {
        Task<IEnumerable<Role>> GetAll();
        Task<Role?> GetOne(int id);
        Task Insert(Role role);
        void Update(Role role);
        void Delete(Role role);
    }
}
