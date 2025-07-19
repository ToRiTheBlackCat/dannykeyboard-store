using DannyKeyboard.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DannyKeyboard.Domain.Interfaces
{
    public interface IShiftRepository
    {
        Task<IEnumerable<Shift>> GetAll();
        Task<Shift?> GetOne(int id);
        Task Insert(Shift shift);
        void Update(Shift shift);
        void Delete(Shift shift);
    }
}
