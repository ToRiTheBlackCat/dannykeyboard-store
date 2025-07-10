using DannyKeyboard.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DannyKeyboard.Domain.Interfaces
{
    public interface IAboutUsRepository
    {
        Task<IEnumerable<AboutUs>> GetAll();
        Task<AboutUs?> GetOne(int id);
        Task Insert(AboutUs aboutUs);
        void Update(AboutUs aboutUs);
        void Delete(AboutUs aboutUs);
    }
}
