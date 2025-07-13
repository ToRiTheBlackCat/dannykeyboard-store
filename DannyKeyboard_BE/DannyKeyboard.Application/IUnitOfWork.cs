using DannyKeyboard.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DannyKeyboard.Application
{
    public interface IUnitOfWork : IDisposable
    {
        IAboutUsRepository AboutUsRepo { get; }
        IUserRepository UserRepo { get; }
        IPolicyRepository PolicyRepo { get; }

        Task BeginTransactionAsync();
        Task CommitTransactionAsync();
        Task RollbackTransactionAsync();
        Task<int> SaveChangesAsync();
    }
}
