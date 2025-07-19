using DannyKeyboard.Application;
using DannyKeyboard.Domain.Interfaces;
using DannyKeyboard.Infrastructure.Context;
using DannyKeyboard.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DannyKeyboard.Infrastructure.Base
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DannyKeyboardShopDBContext _context;
        private IDbContextTransaction? _transaction;

        #region Register_Repo
        public IAboutUsRepository AboutUsRepo { get; }
        public IUserRepository UserRepo { get; }
        public IPolicyRepository PolicyRepo { get; }
        public ICustomerRepository CustomerRepo { get; }
        public IStaffRepository StaffRepo { get; }
        public IRoleRepository RoleRepo { get; }
        public IShiftRepository ShiftRepo { get; }

        #endregion

        public UnitOfWork(DannyKeyboardShopDBContext context)
        {
            _context = context;

            #region Register_Repo
            AboutUsRepo = new AboutUsRepository(_context);
            UserRepo = new UserRepository(_context);
            PolicyRepo = new PolicyRepository(_context);
            CustomerRepo = new CustomerRepository(_context);
            StaffRepo = new StaffRepository(_context);
            RoleRepo = new RoleRepository(_context);
            ShiftRepo = new ShiftRepository(_context);
            #endregion
        }

        public async Task BeginTransactionAsync()
        {
            if (_transaction == null)
            {
                _transaction = await _context.Database.BeginTransactionAsync();
            }
        }

        public async Task CommitTransactionAsync()
        {
            if (_transaction != null)
            {
                await _context.SaveChangesAsync();
                await _transaction.CommitAsync();
                await _transaction.DisposeAsync();
                _transaction = null;
            }
        }

        public async Task RollbackTransactionAsync()
        {
            if (_transaction != null)
            {
                await _transaction.RollbackAsync();
                await _transaction.DisposeAsync();
                _transaction = null;
            }
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }

}
