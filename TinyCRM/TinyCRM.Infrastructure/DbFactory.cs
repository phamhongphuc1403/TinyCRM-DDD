﻿using Microsoft.EntityFrameworkCore;
using TinyCRM.Infrastructure.Data;

namespace TinyCRM.Infrastructure
{
    public class DbFactory : IDisposable
    {
        private bool _disposed;
        private readonly Func<AppDbContext> _instanceFunc;
        private DbContext? _dbContext;
        public DbContext DbContext => _dbContext ??= _instanceFunc.Invoke();

        public DbFactory(Func<AppDbContext> dbContextFactory)
        {
            _instanceFunc = dbContextFactory;
        }

        public void Dispose()
        {
            if (!_disposed && _dbContext != null)
            {
                _disposed = true;
                _dbContext.Dispose();
            }
        }
    }
}