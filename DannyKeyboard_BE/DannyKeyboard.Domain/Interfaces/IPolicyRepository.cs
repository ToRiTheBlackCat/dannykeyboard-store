using DannyKeyboard.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DannyKeyboard.Domain.Interfaces
{
    public interface IPolicyRepository
    {
        Task<IEnumerable<Policy>> GetAll();
        Task<Policy?> GetOne(int id);
        Task Insert(Policy policy);
        void Update(Policy policy);
        void Delete(Policy policy);
    }
}
